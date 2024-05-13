CREATE PROCEDURE [dbo].[get_document_byid]
	@Id INT,
    @UserId INT
AS
	SELECT 
	   d.[Id]
      ,d.[UserId]
      ,[StreamId]
      ,[DocumentTypeId]
      ,DocumentName
      ,dt.Name as DocumentType
      ,[InternalViewOnly]
      ,d.[CreatedByUserId]
      ,u.UserName as UploadedBy
      ,d.CreatedAtUtc as UploadedDateUtc
      ,d.[CreatedAtUtc]
      ,d.[LastUpdatedAtUtc]
      ,d.[LastUpdatedByUserId]
    FROM
        [dbo].[user_documents] d
        inner join document_types dt on dt.id = d.DocumentTypeId
        inner join users u on u.UserId = d.UserId
    WHERE
        d.Id = @Id
        AND d.UserId = @UserId
        AND InternalViewOnly = 0