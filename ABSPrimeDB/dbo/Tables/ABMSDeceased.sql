
CREATE TABLE [dbo].[ABMSDeceased] (
    [UserId]        INT                   NULL,
    [candidate]     CHAR(6)               PRIMARY KEY,
    [ABMSID]        CHAR(8)               NOT NULL,
    [first_name]    VARCHAR (30)          NOT NULL,
    [middle_name]   VARCHAR (30)          NULL,
    [last_name]     VARCHAR (30)          NOT NULL,
    [suffix]        VARCHAR (20)          NULL,
    [deceased_date] DATETIME              NOT NULL,
    CONSTRAINT [FK_abmsdeceased_users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);     