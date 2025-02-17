using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using View.Dto;

namespace View.ApiRoutes;

public static class Map
{
    public static void Get(WebApplication app)
    {
        #region Aircrafts

        app.MapGet("/api/aircrafts", async (
            [FromQuery] int? fuel,
            [FromQuery] int? speed,
            [FromQuery] int? altitude,
            [FromQuery] string? name,
            [FromQuery] int? specificationId,
            IRepository<Aircraft> repository
        ) =>
        {
            var aircrafts = await repository.ReadAllAsync();

            // Filter anwenden, falls Parameter vorhanden sind
            if (fuel is not null) aircrafts = aircrafts.Where(a => a.Fuel == fuel).ToList();
            if (speed is not null) aircrafts = aircrafts.Where(a => a.Speed == speed).ToList();
            if (altitude is not null) aircrafts = aircrafts.Where(a => a.Altitude == altitude).ToList();
            if (!string.IsNullOrWhiteSpace(name)) aircrafts = aircrafts.Where(a => a.Name.Contains(name)).ToList();
            if (specificationId is not null)
                aircrafts = aircrafts.Where(a => a.SpecificationId == specificationId).ToList();

            return Results.Ok(aircrafts);
        });
        app.MapGet("/api/aircrafts/{id}", async (int id, IRepository<Aircraft> repository) =>
        {
            var aircraft = await repository.ReadAsync(id);
            return aircraft is not null ? Results.Ok(aircraft) : Results.NotFound();
        });

        #endregion

        #region AircraftCrew

        app.MapGet("/api/aircraftcrew", async (
            [FromQuery] int? aircraftId,
            [FromQuery] int? mercenaryId,
            [FromQuery] DateTime? joinedAt,
            IRepository<AircraftCrew> repository
        ) =>
        {
            var crews = await repository.ReadAllAsync();

            if (aircraftId is not null) crews = crews.Where(c => c.AircraftId == aircraftId).ToList();
            if (mercenaryId is not null) crews = crews.Where(c => c.MercenaryId == mercenaryId).ToList();
            if (joinedAt is not null) crews = crews.Where(c => c.JoinedAt.Date == joinedAt.Value.Date).ToList();

            var result = crews.Select(c => new AircraftCrewDto
            {
                AircraftId = c.AircraftId,
                MercenaryId = c.MercenaryId,
                JoinedAt = c.JoinedAt
            }).ToList();

            return Results.Ok(result);
        });

        #endregion

        #region AircraftSpecification

        app.MapGet("/api/aircraftspecifications/{id}", async (int id, IRepository<AircraftSpecification> repository) =>
        {
            var specification = await repository.ReadAsync(id);
            return specification is not null ? Results.Ok(specification) : Results.NotFound();
        });
        app.MapGet("/api/aircraftspecifications", async (
            [FromQuery] int? structure,
            [FromQuery] int? fuelTankCapacity,
            [FromQuery] int? minSpeed,
            [FromQuery] int? maxSpeed,
            [FromQuery] int? maxAltitude,
            [FromQuery] string? specificationCode,
            IRepository<AircraftSpecification> repository
        ) =>
        {
            var specifications = await repository.ReadAllAsync();

            if (structure is not null) specifications = specifications.Where(s => s.Structure == structure).ToList();
            if (fuelTankCapacity is not null)
                specifications = specifications.Where(s => s.FuelTankCapacity == fuelTankCapacity).ToList();
            if (minSpeed is not null) specifications = specifications.Where(s => s.MinSpeed >= minSpeed).ToList();
            if (maxSpeed is not null) specifications = specifications.Where(s => s.MaxSpeed <= maxSpeed).ToList();
            if (maxAltitude is not null)
                specifications = specifications.Where(s => s.MaxAltitude == maxAltitude).ToList();
            if (!string.IsNullOrWhiteSpace(specificationCode))
                specifications = specifications.Where(s => s.SpecificationCode.Contains(specificationCode)).ToList();

            return Results.Ok(specifications);
        });

        #endregion

        #region Compartment

        app.MapGet("/api/compartments/{id}", async (int id, IRepository<Compartment> repository) =>
        {
            var compartment = await repository.ReadAsync(id);
            return compartment is not null ? Results.Ok(compartment) : Results.NotFound();
        });
        app.MapGet("/api/compartments", async (
            [FromQuery] int? aircraftId,
            IRepository<Compartment> repository
        ) =>
        {
            var compartments = await repository.ReadAllAsync();

            if (aircraftId is not null) compartments = compartments.Where(c => c.AircraftId == aircraftId).ToList();

            return Results.Ok(compartments);
        });

        #endregion

        #region CrimeSyndicate

        app.MapGet("/api/crimesyndicates/{id}", async (int id, IRepository<CrimeSyndicate> repository) =>
        {
            var syndicate = await repository.ReadAsync(id);
            return syndicate is not null ? Results.Ok(syndicate) : Results.NotFound();
        });
        app.MapGet("/api/crimesyndicates", async (
            [FromQuery] string? name,
            [FromQuery] string? location,
            IRepository<CrimeSyndicate> repository
        ) =>
        {
            var syndicates = await repository.ReadAllAsync();

            if (!string.IsNullOrWhiteSpace(name)) syndicates = syndicates.Where(s => s.Name.Contains(name)).ToList();
            if (!string.IsNullOrWhiteSpace(location))
                syndicates = syndicates.Where(s => s.Location.Contains(location)).ToList();

            return Results.Ok(syndicates);
        });

        #endregion

        #region Machinery

        app.MapGet("/api/machineries/{id}", async (int id, IRepository<Machinery> repository) =>
        {
            var machinery = await repository.ReadAsync(id);
            if (machinery is null)
                return Results.NotFound();

            // Optional: Discriminator-Feld explizit anpassen
            var result = new
            {
                machinery.Id,
                machinery.Label,
                machinery.Function,
                machinery.CompartmentId,
                MachineryType = machinery.GetType().Name // Hier den Typ als Discriminator hinzufügen
            };

            return Results.Ok(result);
        });
        app.MapGet("/api/machineries", async (
            [FromQuery] string? label,
            [FromQuery] string? function,
            [FromQuery] int? compartmentId,
            IRepository<Machinery> repository
        ) =>
        {
            var machineries = await repository.ReadAllAsync();

            if (!string.IsNullOrWhiteSpace(label))
                machineries = machineries.Where(m => m.Label.Contains(label)).ToList();
            if (!string.IsNullOrWhiteSpace(function))
                machineries = machineries.Where(m => m.Function.Contains(function)).ToList();
            if (compartmentId is not null)
                machineries = machineries.Where(m => m.CompartmentId == compartmentId).ToList();

            // Optional: Discriminator-Feld für alle Maschinen hinzufügen
            var result = machineries.Select(m => new
            {
                m.Id,
                m.Label,
                m.Function,
                m.CompartmentId,
                MachineryType = m.GetType().Name // Hinzufügen des Maschinen-Typs
            }).ToList();

            return Results.Ok(result);
        });

        #endregion

        #region Mercenary

        app.MapGet("/api/mercenaries/{id}", async (int id, IRepository<Mercenary> repository) =>
        {
            var mercenary = await repository.ReadAsync(id);
            return mercenary is not null ? Results.Ok(mercenary) : Results.NotFound();
        });
        app.MapGet("/api/mercenaries", async (
            [FromQuery] string? firstName,
            [FromQuery] string? lastName,
            [FromQuery] int? bodySkills,
            [FromQuery] int? combatSkills,
            IRepository<Mercenary> repository
        ) =>
        {
            var mercenaries = await repository.ReadAllAsync();

            // Filtern der Ergebnisse anhand der Query-Parameter
            if (!string.IsNullOrWhiteSpace(firstName))
                mercenaries = mercenaries.Where(m => m.FirstName.Contains(firstName)).ToList();
            if (!string.IsNullOrWhiteSpace(lastName))
                mercenaries = mercenaries.Where(m => m.LastName.Contains(lastName)).ToList();
            if (bodySkills is not null)
                mercenaries = mercenaries.Where(m => m.BodySkills == bodySkills).ToList();
            if (combatSkills is not null)
                mercenaries = mercenaries.Where(m => m.CombatSkills == combatSkills).ToList();

            return Results.Ok(mercenaries);
        });

        #endregion

        #region MercenaryReputation

        app.MapGet("/api/mercenaryreputations", async (
            [FromQuery] int? syndicateId,
            [FromQuery] int? mercenaryId,
            [FromQuery] string? reputationChange,
            IRepository<MercenaryReputation> repository
        ) =>
        {
            var reputations = await repository.ReadAllAsync();

            // Filtern der Ergebnisse anhand der Query-Parameter
            if (syndicateId is not null) reputations = reputations.Where(r => r.SyndicateId == syndicateId).ToList();
            if (mercenaryId is not null) reputations = reputations.Where(r => r.MercenaryId == mercenaryId).ToList();
            if (!string.IsNullOrWhiteSpace(reputationChange))
                reputations = reputations.Where(r => r.ReputationChange.Contains(reputationChange)).ToList();

            // Mapping der Ergebnisse zu den DTOs
            var result = reputations.Select(r => new MercenaryReputationDto
            {
                SyndicateId = r.SyndicateId,
                MercenaryId = r.MercenaryId,
                ReputationChange = r.ReputationChange
            }).ToList();

            return Results.Ok(result);
        });

        #endregion
    }

    public static void Post(WebApplication app)
    {
        #region Aircrafts

        app.MapPost("/api/aircrafts", async (AircraftDto aircraftDto, IRepository<Aircraft> repository) =>
        {
            var aircraft = new Aircraft
            {
                Fuel = aircraftDto.Fuel,
                Speed = aircraftDto.Speed,
                Altitude = aircraftDto.Altitude,
                Name = aircraftDto.Name,
                SpecificationId = aircraftDto.SpecificationId
            };
            await repository.CreateAsync(aircraft);
            return Results.Created($"/api/aircrafts/{aircraft.Id}", aircraft);
        });

        #endregion

        #region AircraftCrew

        app.MapPost("/api/aircraftcrew", async (AircraftCrewDto crewDto, IRepository<AircraftCrew> repository) =>
        {
            var crew = new AircraftCrew
            {
                AircraftId = crewDto.AircraftId,
                MercenaryId = crewDto.MercenaryId,
                JoinedAt = crewDto.JoinedAt
            };

            await repository.CreateAsync(crew);
            return Results.Created($"/api/aircraftcrew?aircraftId={crew.AircraftId}&mercenaryId={crew.MercenaryId}",
                crew);
        });

        #endregion

        #region AircraftSpecification

        app.MapPost("/api/aircraftspecifications",
            async (AircraftSpecificationDto specDto, IRepository<AircraftSpecification> repository) =>
            {
                var specification = new AircraftSpecification
                {
                    Structure = specDto.Structure,
                    FuelTankCapacity = specDto.FuelTankCapacity,
                    MinSpeed = specDto.MinSpeed,
                    MaxSpeed = specDto.MaxSpeed,
                    MaxAltitude = specDto.MaxAltitude,
                    SpecificationCode = specDto.SpecificationCode
                };
                await repository.CreateAsync(specification);
                return Results.Created($"/api/aircraftspecifications/{specification.Id}", specification);
            });

        #endregion

        #region Compartment

        app.MapPost("/api/compartments", async (CompartmentDto compartmentDto, IRepository<Compartment> repository) =>
        {
            var compartment = new Compartment
            {
                AircraftId = compartmentDto.AircraftId
            };
            await repository.CreateAsync(compartment);
            return Results.Created($"/api/compartments/{compartment.Id}", compartment);
        });

        #endregion

        #region CrimeSyndicate

        app.MapPost("/api/crimesyndicates",
            async (CrimeSyndicateDto syndicateDto, IRepository<CrimeSyndicate> repository) =>
            {
                var syndicate = new CrimeSyndicate
                {
                    Name = syndicateDto.Name,
                    Location = syndicateDto.Location
                };
                await repository.CreateAsync(syndicate);
                return Results.Created($"/api/crimesyndicates/{syndicate.Id}", syndicate);
            });

        #endregion

        #region Machinery

        app.MapPost("/api/machineries", async (MachineryDto machineryDto, IRepository<Machinery> repository) =>
        {
            Machinery machinery;

            if (machineryDto.MachineryType == "EnergySystem")
                machinery = new EnergySystem
                {
                    Label = machineryDto.Label,
                    Function = machineryDto.Function,
                    CompartmentId = machineryDto.CompartmentId
                };
            else if (machineryDto.MachineryType == "EnviromentalSystem")
                machinery = new EnviromentalSystem
                {
                    Label = machineryDto.Label,
                    Function = machineryDto.Function,
                    CompartmentId = machineryDto.CompartmentId
                };
            else
                machinery = new Weapon
                {
                    Label = machineryDto.Label,
                    Function = machineryDto.Function,
                    CompartmentId = machineryDto.CompartmentId
                };

            await repository.CreateAsync(machinery);
            return Results.Created($"/api/machineries/{machinery.Id}", machinery);
        });

        #endregion

        #region Mercenary

        app.MapPost("/api/mercenaries", async (MercenaryDto mercenaryDto, IRepository<Mercenary> repository) =>
        {
            var mercenary = new Mercenary
            {
                FirstName = mercenaryDto.FirstName,
                LastName = mercenaryDto.LastName,
                BodySkills = mercenaryDto.BodySkills,
                CombatSkills = mercenaryDto.CombatSkills
            };
            await repository.CreateAsync(mercenary);
            return Results.Created($"/api/mercenaries/{mercenary.Id}", mercenary);
        });

        #endregion

        #region MercenaryReputation

        app.MapPost("/api/mercenaryreputations",
            async (MercenaryReputationDto reputationDto, IRepository<MercenaryReputation> repository) =>
            {
                var reputation = new MercenaryReputation
                {
                    SyndicateId = reputationDto.SyndicateId,
                    MercenaryId = reputationDto.MercenaryId,
                    ReputationChange = reputationDto.ReputationChange
                };

                await repository.CreateAsync(reputation);
                return Results.Created(
                    $"/api/mercenaryreputations?syndicateId={reputation.SyndicateId}&mercenaryId={reputation.MercenaryId}",
                    reputation);
            });

        #endregion
    }

    public static void Put(WebApplication app)
    {
        #region Aircraft

        app.MapPut("/api/aircrafts/{id}", async (
            int id,
            AircraftDto aircraftDto,
            IRepository<Aircraft> repository
        ) =>
        {
            var aircraft = await repository.ReadAsync(id);

            if (aircraft is null)
            {
                return Results.NotFound(); // Falls der Datensatz nicht existiert
            }

            // Aktualisieren der Daten mit den Werten aus dem DTO
            aircraft.Fuel = aircraftDto.Fuel;
            aircraft.Speed = aircraftDto.Speed;
            aircraft.Altitude = aircraftDto.Altitude;
            aircraft.Name = aircraftDto.Name;
            aircraft.SpecificationId = aircraftDto.SpecificationId;

            // Speichern der Änderungen in der Datenbank
            await repository.UpdateAsync(id, aircraft);

            return Results.Ok(aircraft); // Erfolgreiches Update
        });

        #endregion

        #region AircraftCrew

        app.MapPut("/api/aircraftcrew", async (
            AircraftCrewDto aircraftCrewDto,
            IRepository<AircraftCrew> repository
        ) =>
        {
            var crew = await repository.ReadAsync([aircraftCrewDto.AircraftId, aircraftCrewDto.MercenaryId]);

            if (crew is null)
            {
                return Results.NotFound(); // Falls der Datensatz nicht existiert
            }

            // Aktualisieren der Daten mit den Werten aus dem DTO
            crew.JoinedAt = aircraftCrewDto.JoinedAt;

            // Speichern der Änderungen in der Datenbank
            await repository.UpdateAsync([aircraftCrewDto.AircraftId, aircraftCrewDto.MercenaryId], crew);

            return Results.Ok(crew); // Erfolgreiches Update
        });

        #endregion

        #region Compartment

        app.MapPut("/api/compartments/{id}", async (
            int id,
            CompartmentDto compartmentDto,
            IRepository<Compartment> repository
        ) =>
        {
            var compartment = await repository.ReadAsync(id);

            if (compartment is null)
            {
                return Results.NotFound(); // Falls der Datensatz nicht existiert
            }

            // Aktualisieren der Daten mit den Werten aus dem DTO
            compartment.AircraftId = compartmentDto.AircraftId;

            // Speichern der Änderungen in der Datenbank
            await repository.UpdateAsync(id, compartment);

            return Results.Ok(compartment); // Erfolgreiches Update
        });

        #endregion

        #region CrimeSyndicate

        app.MapPut("/api/crimesyndicates/{id}", async (
            int id,
            CrimeSyndicateDto crimeSyndicateDto,
            IRepository<CrimeSyndicate> repository
        ) =>
        {
            var syndicate = await repository.ReadAsync(id);

            if (syndicate is null)
            {
                return Results.NotFound(); // Falls der Datensatz nicht existiert
            }

            // Aktualisieren der Daten mit den Werten aus dem DTO
            syndicate.Name = crimeSyndicateDto.Name;
            syndicate.Location = crimeSyndicateDto.Location;

            // Speichern der Änderungen in der Datenbank
            await repository.UpdateAsync(id, syndicate);

            return Results.Ok(syndicate); // Erfolgreiches Update
        });

        #endregion

        #region Machinery

        app.MapPut("/api/machineries/{id}", async (
            int id,
            MachineryDto machineryDto,
            IRepository<Machinery> repository
        ) =>
        {
            var machinery = await repository.ReadAsync(id);

            if (machinery is null)
            {
                return Results.NotFound(); // Falls der Datensatz nicht existiert
            }

            // Aktualisieren der Daten mit den Werten aus dem DTO
            machinery.Label = machineryDto.Label;
            machinery.Function = machineryDto.Function;
            machinery.CompartmentId = machineryDto.CompartmentId;

            // Speichern der Änderungen in der Datenbank
            await repository.UpdateAsync(id, machinery);

            return Results.Ok(machinery); // Erfolgreiches Update
        });

        #endregion

        #region Mercenary

        app.MapPut("/api/mercenaries/{id}", async (
            int id,
            MercenaryDto mercenaryDto,
            IRepository<Mercenary> repository
        ) =>
        {
            var mercenary = await repository.ReadAsync(id);

            if (mercenary is null)
            {
                return Results.NotFound(); // Falls der Datensatz nicht existiert
            }

            // Aktualisieren der Daten mit den Werten aus dem DTO
            mercenary.FirstName = mercenaryDto.FirstName;
            mercenary.LastName = mercenaryDto.LastName;
            mercenary.BodySkills = mercenaryDto.BodySkills;
            mercenary.CombatSkills = mercenaryDto.CombatSkills;

            // Speichern der Änderungen in der Datenbank
            await repository.UpdateAsync(id, mercenary);

            return Results.Ok(mercenary); // Erfolgreiches Update
        });

        #endregion

        #region MercenaryReputation

        app.MapPut("/api/mercenaryreputations", async (
            MercenaryReputationDto mercenaryReputationDto,
            IRepository<MercenaryReputation> repository
        ) =>
        {
            var reputation =
                await repository.ReadAsync([mercenaryReputationDto.SyndicateId, mercenaryReputationDto.MercenaryId]);

            if (reputation is null)
            {
                return Results.NotFound(); // Falls der Datensatz nicht existiert
            }

            // Aktualisieren der Daten mit den Werten aus dem DTO
            reputation.ReputationChange = mercenaryReputationDto.ReputationChange;

            // Speichern der Änderungen in der Datenbank
            await repository.UpdateAsync([mercenaryReputationDto.SyndicateId, mercenaryReputationDto.MercenaryId],
                reputation);

            return Results.Ok(reputation); // Erfolgreiches Update
        });

        #endregion

        #region AircraftSpecification

        app.MapPut("/api/aircraftspecifications/{id}", async (
            int id,
            AircraftSpecificationDto aircraftSpecificationDto,
            IRepository<AircraftSpecification> repository
        ) =>
        {
            var specification = await repository.ReadAsync(id);

            if (specification is null)
            {
                return Results.NotFound(); // Falls der Datensatz nicht existiert
            }

            // Aktualisieren der Daten mit den Werten aus dem DTO
            specification.Structure = aircraftSpecificationDto.Structure;
            specification.FuelTankCapacity = aircraftSpecificationDto.FuelTankCapacity;
            specification.MinSpeed = aircraftSpecificationDto.MinSpeed;
            specification.MaxSpeed = aircraftSpecificationDto.MaxSpeed;
            specification.MaxAltitude = aircraftSpecificationDto.MaxAltitude;
            specification.SpecificationCode = aircraftSpecificationDto.SpecificationCode;

            // Speichern der Änderungen in der Datenbank
            await repository.UpdateAsync(id, specification);

            return Results.Ok(specification); // Erfolgreiches Update
        });

        #endregion
    }

    public static void Delete(WebApplication app)
    {
        #region Aircraft

        app.MapDelete("/api/aircrafts/{id}", async (
            int id,
            IRepository<Aircraft> repository
        ) =>
        {
            var aircraft = await repository.ReadAsync(id);
            if (aircraft is null)
                return Results.NotFound();

            await repository.DeleteAsync(id);
            return Results.Ok();
        });

        #endregion

        #region AircraftCrew

        app.MapDelete("/api/aircraftcrew", async (
            [FromQuery] int aircraftId,
            [FromQuery] int mercenaryId,
            IRepository<AircraftCrew> repository
        ) =>
        {
            var crew = await repository.ReadAsync([aircraftId, mercenaryId]);
            if (crew is null)
                return Results.NotFound();

            await repository.DeleteAsync([aircraftId, mercenaryId]);
            return Results.Ok();
        });

        #endregion

        #region AircraftSpecification

        app.MapDelete("/api/aircraftspecifications/{id}", async (
            int id,
            IRepository<AircraftSpecification> repository
        ) =>
        {
            var specification = await repository.ReadAsync(id);
            if (specification is null)
                return Results.NotFound();

            await repository.DeleteAsync(id);
            return Results.Ok();
        });

        #endregion

        #region Compartment

        app.MapDelete("/api/compartments/{id}", async (
            int id,
            IRepository<Compartment> repository
        ) =>
        {
            var compartment = await repository.ReadAsync(id);
            if (compartment is null)
                return Results.NotFound();

            await repository.DeleteAsync(id);
            return Results.Ok();
        });

        #endregion

        #region CrimeSyndicate

        app.MapDelete("/api/crimesyndicates/{id}", async (
            int id,
            IRepository<CrimeSyndicate> repository
        ) =>
        {
            var syndicate = await repository.ReadAsync(id);
            if (syndicate is null)
                return Results.NotFound();

            await repository.DeleteAsync(id);
            return Results.Ok();
        });

        #endregion

        #region Machinery

        app.MapDelete("/api/machineries/{id}", async (
            int id,
            IRepository<Machinery> repository
        ) =>
        {
            var machinery = await repository.ReadAsync(id);
            if (machinery is null)
                return Results.NotFound();

            await repository.DeleteAsync(id);
            return Results.Ok();
        });

        #endregion

        #region Mercenary

        app.MapDelete("/api/mercenaries/{id}", async (
            int id,
            IRepository<Mercenary> repository
        ) =>
        {
            var mercenary = await repository.ReadAsync(id);
            if (mercenary is null)
                return Results.NotFound();

            await repository.DeleteAsync(id);
            return Results.Ok();
        });

        #endregion

        #region MercenaryReputation

        app.MapDelete("/api/mercenaryreputations", async (
            [FromQuery] int syndicateId,
            [FromQuery] int mercenaryId,
            IRepository<MercenaryReputation> repository
        ) =>
        {
            var reputation = await repository.ReadAsync([syndicateId, mercenaryId]);
            if (reputation is null)
                return Results.NotFound();

            await repository.DeleteAsync([syndicateId, mercenaryId]);
            return Results.Ok();
        });

        #endregion
    }
}