insert into exam_formats(Code, name,  createdbyuserid, lastupdatedbyuserid)
VALUES ('W','In-Person Written Examination (Pearson)',@UserId,@UserId),
('O','Online Oral Examination',@UserId,@UserId),
('R','Online Written Examination (ITS)', @UserId, @UserId)