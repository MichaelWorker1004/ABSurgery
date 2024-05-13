INSERT INTO appointment_types(name, createdbyuserid, lastupdatedbyuserid)
VALUES
('Active staff', @UserId, @UserId),
('Associate/Consulting staff', @UserId, @UserId),
('Bioscientific/Research staff', @UserId, @UserId),
('Locum tenens', @UserId, @UserId),
('Other', @UserId, @UserId)