CREATE PROCEDURE [dbo].[insert_exams]
    @ExamDate DATETIME,
    @ExamCode VARCHAR(50),
    @ExaminerAbsId VARCHAR(50),
    @AssociateExaminerAbsId VARCHAR(50),
    @ExamineeAbsId VARCHAR(50),
    @Case1Number VARCHAR(50),
    @Case2Number VARCHAR(50),
    @Case3Number VARCHAR(50),
    @Case4Number VARCHAR(50),
    @Day INT,
    @Session INT,
    @StartTime TIME,
    @EndTime TIME,
    @Roster VARCHAR(1),
    @Row INT
AS
    DECLARE @ExaminerUserId INT
    DECLARE @AssociateExaminerUserId INT
    DECLARE @ExamineeUserId INT
    SELECT @ExaminerUserId = UserId FROM user_profiles WHERE AbsId = @ExaminerAbsId
    SELECT @AssociateExaminerUserId = UserId FROM user_profiles WHERE AbsId = @AssociateExaminerAbsId
    SELECT @ExamineeUserId = UserId FROM user_profiles WHERE AbsId = @ExamineeAbsId

    DECLARE @Case1Id INT
    DECLARE @Case2Id INT
    DECLARE @Case3Id INT
    DECLARE @Case4Id INT
    SELECT @Case1Id = Id FROM case_headers WHERE CaseNumber = @Case1Number
    SELECT @Case2Id = Id FROM case_headers WHERE CaseNumber = @Case2Number
    SELECT @Case3Id = Id FROM case_headers WHERE CaseNumber = @Case3Number
    SELECT @Case4Id = Id FROM case_headers WHERE CaseNumber = @Case4Number

    INSERT INTO exam_schedules
    (
        ExamDate,
        DayNumber,
        RowNumber,
        SessionNumber,
        StartTime,
        EndTime,
        ExaminerUserId,
        AssociateExaminerUserId,
        ExamineeUserId,
        Roster,
        CreatedByUserId,
        LastUpdatedByUserId
    )
    VALUES
    (
        @ExamDate,
        @Day,
        @Row,
        @Session,
        @StartTime,
        @EndTime,
        @ExaminerUserId,
        @AssociateExaminerUserId,
        @ExamineeUserId,
        @Roster,
        6,
        6
    )

    DECLARE @ExamScheduleId INT
    SET @ExamScheduleId = SCOPE_IDENTITY()

    INSERT INTO exam_schedule_links
    (
        ExamScheduleId,
        ExamHeaderId,
        CreatedByUserId,
        LastUpdatedByUserId
    )
    VALUES
    (
        @ExamScheduleId,
        (SELECT Id FROM exam_directory WHERE ExamCode = @ExamCode),
        6,
        6
    )
    INSERT INTO Exam_Cases
    (
        ExamScheduleId,
        CaseId,
        SortOrder,
        CreatedByUserId,
        LastUpdatedByUserId
    )
    VALUES
    (
        @ExamScheduleId,
        @Case1Id,
        1,
        6,
        6
    ),
    (
        @ExamScheduleId,
        @Case2Id,
        2,
        6,
        6
    ),
    (
        @ExamScheduleId,
        @Case3Id,
        3,
        6,
        6
    ),
    (
        @ExamScheduleId,
        @Case4Id,
        4,
        6,
        6
    )
    INSERT INTO user_claims
    (
        ApplicationClaimId,
        UserId,
        CreatedByUserId,
        LastUpdatedByUserId
    )
    SELECT
        4,
        @ExaminerUserId,
        6,
        6
    WHERE NOT EXISTS (SELECT * FROM user_claims WHERE ApplicationClaimId = 4 AND UserId = @ExaminerUserId)

    INSERT INTO user_claims
    (
        ApplicationClaimId,
        UserId,
        CreatedByUserId,
        LastUpdatedByUserId
    )
    SELECT
        4,
        @AssociateExaminerUserId,
        6,
        6
    WHERE NOT EXISTS (SELECT * FROM user_claims WHERE ApplicationClaimId = 4 AND UserId = @AssociateExaminerUserId)

