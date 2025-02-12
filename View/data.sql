-- Beispiel-Daten für AIRCRAFT_SPECIFICATIONS
INSERT INTO AIRCRAFT_SPECIFICATIONS (Structure, FuelTankCapacity, MinSpeed, MaxSpeed, MaxAltitude, SpezificationCode)
VALUES
    (100, 5000, 200, 900, 12000, 'SPC-001'),
    (120, 6000, 250, 1000, 15000, 'SPC-002');

-- Beispiel-Daten für AIRCRAFTS
INSERT INTO AIRCRAFTS (Fuel, Speed, Altitude, Name, SpecificationId)
VALUES
    (3000, 400, 5000, 'Falcon', 1),
    (4500, 600, 8000, 'Raptor', 2);

-- Beispiel-Daten für COMPARTMENTS
INSERT INTO COMPARTMENTS (AircraftId)
VALUES
    (1),
    (2);

-- Beispiel-Daten für CRIME_SYNDICATES
INSERT INTO CRIME_SYNDICATES (Name, Location)
VALUES
    ('Black Cobra', 'New York'),
    ('Red Scorpions', 'Los Angeles');

-- Beispiel-Daten für MACHINERIES
INSERT INTO MACHINERIES (Label, Function, CompartmentId, MachineryType)
VALUES
    ('Engine X', 'Propulsion', 1, 'EnergySystem'),
    ('Battery Pack', 'Power Supply', 1, 'EnergySystem'),
    ('Oxygen System', 'Life Support', 2, 'EnviromentalSystem'),
    ('Cooling Unit', 'Temperature Control', 2, 'EnviromentalSystem'),
    ('Missile Launcher', 'Offensive Weapon', 1, 'Weapon'),
    ('Machine Gun', 'Defensive Weapon', 2, 'Weapon');

-- Beispiel-Daten für MERCENARIES
INSERT INTO MERCENARIES (FirstName, LastName, BodySkills, CombatSkills)
VALUES
    ('John', 'Doe', 80, 90),
    ('Jane', 'Smith', 85, 95);

-- Beispiel-Daten für AIRCRAFT_CREW_JT
INSERT INTO AIRCRAFT_CREW_JT (AircraftId, MercenaryId, JoinedAt)
VALUES
    (1, 1, '2025-01-15'),
    (2, 2, '2025-02-01');

-- Beispiel-Daten für MERCENARY_REPUTATIONS
INSERT INTO MERCENARY_REPUTATIONS (SyndicateId, MercenaryId, ReputationChange)
VALUES
    (1, 1, 'Gained trust'),
    (2, 2, 'Lost favor');