insert into exam_statuses(Code, name,  createdbyuserid, lastupdatedbyuserid)
VALUES ('R','Registered',@UserId,@UserId),
('N','Cancelled',@UserId,@UserId),
('C','Completed',@UserId,@UserId),
('X','Expired',@UserId,@UserId),
('P','Postponed',@UserId,@UserId),
('T','Tentative Assignment',@UserId,@UserId),
('W','Wait List',@UserId,@UserId)
       