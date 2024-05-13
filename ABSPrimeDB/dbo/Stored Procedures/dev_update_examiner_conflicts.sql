CREATE PROCEDURE [dev_update_examiner_conflicts]
@ExamineeABSId VARCHAR(10),
@ExaminerABSId VARCHAR(10),
@ReplacmentABSId VARCHAR(10),
@DayNumber INT
AS
DECLARE @ExamineeUserId INT = (SELECT UserId FROM  user_profiles WHERE ABSId=@ExamineeABSId)
DECLARE @ExaminerUserId INT= (SELECT UserId FROM  user_profiles WHERE ABSId=@ExaminerABSId)
DECLARE @ReplacmentUserId INT= (SELECT UserId FROM  user_profiles WHERE ABSId=@ReplacmentABSId)
DECLARE @IsAssociateExaminer BIT = (SELECT IIF(ExaminerUserId=@ExaminerUserId,0,1) FROM exam_schedules WHERE (ExaminerUserId=@ExaminerUserId OR AssociateExaminerUserId=@ExaminerUserId) AND ExamineeUserId=@ExamineeUserId)
IF @IsAssociateExaminer=1 BEGIN
    UPDATE exam_schedules SET AssociateExaminerUserId=@ReplacmentUserId WHERE ExamineeUserId=@ExamineeUserId AND AssociateExaminerUserId=@ExaminerUserId AND  DayNumber=@DayNumber
END
ELSE BEGIN
    UPDATE exam_schedules SET ExaminerUserId=@ReplacmentUserId WHERE ExamineeUserId=@ExamineeUserId AND ExaminerUserId=@ExaminerUserId AND DayNumber=@DayNumber
END