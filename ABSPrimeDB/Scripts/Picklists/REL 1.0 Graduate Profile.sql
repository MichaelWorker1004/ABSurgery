INSERT INTO graduate_profile (GraduateProfileId,Description, CreatedByUserId, LastUpdatedByUserId)
VALUES (1,'US or Canada born/US or Canada medical school graduate',@UserId,@UserId),
       (2,'Foreign born/foreign medical school graduate',@UserId,@UserId),
       (3,'US or Canada born/foreign medical school graduate',@UserId,@UserId),
       (4,'School of osteopathy graduate',@UserId,@UserId),
       (5,'Foreign born/US or Canada medical school graduate',@UserId,@UserId)