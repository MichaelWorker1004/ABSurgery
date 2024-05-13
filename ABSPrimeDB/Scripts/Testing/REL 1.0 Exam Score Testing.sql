INSERT INTO Exam_Directory (ExamCode, Description, StartDate, EndDate, CreatedByUserId, LastUpdatedByUserId)
VALUES
('2022GO6', 'Dummy General Certifying Examination', '6/20/2023', '6/23/2023', 101932, 101932)

INSERT INTO exam_schedules
    (ExamDate, DayNumber, SessionNumber, StartTime, ExaminerUserId, AssociateExaminerUserId, ExamineeUserId, MeetingLink, Roster, CreatedByUserId, LastUpdatedByUserId)
VALUES
    ('2023-06-20', 1, 1, '9:00:00', 14, 15, 6, 'https//www.zoom.us', 'A', 101932, 101932),
    ('2023-06-20', 1, 2, '10:00:00', 14, 15, 12, 'https//www.zoom.us', 'B', 101932, 101932)

INSERT INTO case_headers
    (Title, Description, CaseNumber, CreatedByUserId, LastUpdatedByUserId)
VALUES 
    ('Test Case 1', 'Test Description', 'G9999', 101932, 101932),
    ('Test Case 2', 'Test Description 2', 'G9998', 101932, 101932)

INSERT INTO case_contents
    (CaseHeaderId, Heading, Content, SectionNumber, SortOrder, CreatedByUserId, LastUpdatedByUserId)
VALUES
    (2, 'Heading 1', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
        1, 1, 101932, 101932),
    (2, 'Heading 2', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
        2, 2, 101932, 101932),
    (2, 'Heading 3', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
        3, 3, 101932, 101932),
    (3, 'Heading 1', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
        1, 1, 101932, 101932),
    (3, 'Heading 2', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
        2, 2, 101932, 101932),
    (3, 'Heading 3', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
        3, 3, 101932, 101932)

INSERT INTO exam_cases
    (ExamScheduleId, CaseId, SortOrder, CreatedByUserId, LastUpdatedByUserId)
VALUES
    (6, 2, 1, 101932, 101932),
    (6, 3, 2, 101932, 101932),
    (7, 3, 1, 101932, 101932),
    (7, 2, 2, 101932, 101932)

INSERT INTO exam_schedule_links
    (ExamScheduleId, ExamHeaderId, CreatedByUserId, LastUpdatedByUserId)
VALUES
    (6, 492, 101932, 101932),
    (7, 492, 101932, 101932)

INSERT INTO exam_scoring
    (ExamCaseId, ExaminerUserId, ExamineeUserId, Score, CriticalFail, Remarks, Submitted, SubmittedDateTimeUTC, CreatedByUserId, LastUpdatedByUserId)
VALUES
    (1, 14, 6, 3, 0, 'Test Remark 1', 0, GETUTCDATE(), 101932, 101932),
    (1, 15, 6, 2, 0, 'Test Remark 2', 0, GETUTCDATE(), 101932, 101932),
    (2, 14, 6, 1, 0, 'Test Remark 3', 0, GETUTCDATE(), 101932, 101932),
    (2, 15, 6, 1, 0, 'Test Remark 4', 1, GETUTCDATE(), 101932, 101932)