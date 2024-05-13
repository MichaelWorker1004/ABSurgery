CREATE PROCEDURE [dbo].[get_case_details_by_id]
    @CaseHeaderId int,
    @ExaminerUserId int
AS
    SELECT
        case_headers.CaseNumber,
        case_headers.Title AS CaseTitle,
        case_contents.Id AS CaseContentId,
        IIF(ISNULL(case_images.SortOrder,0)>1,'',case_contents.Heading) Heading,
        IIF(ISNULL(case_images.SortOrder,0)>1,'',case_contents.Content) + ' ' + IIF(case_images.Image IS NULL,'',case_images.Image) AS Content,
        user_case_comments.Comments,
        user_case_comments.Id as CaseCommentId,
        case_contents.SectionNumber
    FROM
        case_contents
    LEFT JOIN
            case_headers ON case_headers.Id = case_contents.CaseHeaderId
    LEFT JOIN
            user_case_comments ON user_case_comments.CaseContentId = case_contents.Id
                and user_case_comments.UserId = @ExaminerUserId
    LEFT JOIN
            case_images ON case_images.CaseHeaderId = case_headers.Id
            AND case_contents.SectionNumber = (SELECT MAX(SectionNumber) FROM case_contents WHERE CaseHeaderId = @CaseHeaderId and ISNULL(TRIM(Content), '')!='' )
    WHERE
        case_contents.CaseHeaderId = @CaseHeaderId AND
        (case_contents.Content !='' OR
        case_contents.Heading ='Management Points:')
    ORDER BY case_contents.SectionNumber ASC
go

