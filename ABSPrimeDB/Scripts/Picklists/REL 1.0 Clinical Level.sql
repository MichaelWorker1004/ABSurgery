INSERT INTO dbo.clinical_level
( Name,ClinicalLevel, IsActive, CreatedByUserId, LastUpdatedByUserId)
VALUES
('Clinical Level 1',1,1, @userId,@userId),
('Clinical Level 2',2,1,@userId,@userId),
('Clinical Level 3',3,1,@userId,@userId),
('Clinical Level 4',4,1,@userId,@userId),
('Clinical Level 4 Chief',5,1,@userId,@userId),
('Clinical Level 5',6,1,@userId,@userId),
('Clinical Level 5 Chief',7,1,@userId,@userId),
('Research',8,1,@userId,@userId),
('Other Clinical Fellowship',9,1,@userId,@userId);
