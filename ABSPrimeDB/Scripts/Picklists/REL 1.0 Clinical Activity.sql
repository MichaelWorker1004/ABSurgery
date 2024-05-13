﻿insert into clinical_activity 
(Name, IsActive, IsEssential, IsCredit, CreatedByUserId, LastUpdatedByUserId)
values ('Anesthesia',1,0,1,@userId, @userId), 
('Breast Disease',1,1,1,@userId, @userId), 
('Burn',1,1,1,@userId, @userId), 
('Cardiology',1,0,1,@userId, @userId), 
('Clinical (Non-Surgical)',1,0,0,@userId, @userId), 
('Emergency Department',1,0,1,@userId, @userId), 
('Endoscopy',1,1,1,@userId, @userId), 
('ENT',1,0,1,@userId, @userId), 
('Gastroenterology (Non-Surgical)',1,0,1,@userId, @userId), 
('General Surgery',1,1,1,@userId, @userId), 
('ICU - SICU',1,1,1,@userId, @userId), 
('Neurosurgery',1,0,1,@userId, @userId), 
('Non-Clinical - Conferences/Meetings',1,0,0,@userId, @userId), 
('Non-Clinical - Family Leave',1,0,0,@userId, @userId), 
('Non-Clinical - Interviews',1,0,0,@userId, @userId), 
('Non-Clinical - Military Active Duty',1,0,0,@userId, @userId), 
('Non-Clinical - Research',1,0,0,@userId, @userId), 
('Non-Clinical - Vacation',1,0,0,@userId, @userId), 
('Orthopedics',1,0,1,@userId, @userId), 
('Pathology',1,0,1,@userId, @userId), 
('Pediatric Surgery',1,1,1,@userId, @userId), 
('Plastic Surgery',1,0,1,@userId, @userId), 
('Radiology',1,0,1,@userId, @userId), 
('Thoracic Surgery',1,1,1,@userId, @userId), 
('Transplant',1,1,1,@userId, @userId), 
('Trauma',1,1,1,@userId, @userId), 
('Urology',1,0,1,@userId, @userId), 
('Vascular Surgery',1,1,1,@userId, @userId);