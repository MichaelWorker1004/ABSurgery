insert into exam_types(Code, name,  createdbyuserid, lastupdatedbyuserid)
VALUES ('Q','Qualifying Examination',@UserId,@UserId),
('C','Certifying Examination',@UserId,@UserId),
('R','Continuous Certification Readmission Examination', @UserId, @UserId),
('R','Recertification Examination',@UserId,@UserId),
('I','ABSITE/VSITE Readmission Examination',@UserId,@UserId),
('S','SESAP/VESAP Readmission Examination',@UserId,@UserId),
('X','Readmission',@UserId,@UserId),
('B','Examination',@UserId,@UserId),
('P','Qualifying Examination',@UserId,@UserId)