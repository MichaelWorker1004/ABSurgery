INSERT INTO Organization_Type (Type, CreatedByUserId, LastUpdatedByUserId)
VALUES
    ('Solo practice (1 person)',@UserId,@UserId),
    ('Partnership (2 persons)',@UserId,@UserId),
    ('Surgical Group (over 2)',@UserId,@UserId),
    ('Multidisciplinary Group (3-10)',@UserId,@UserId),
    ('Multidisciplinary Clinic (over 10)',@UserId,@UserId),
    ('Governmental (Military, VA, State, etc.)',@UserId,@UserId)