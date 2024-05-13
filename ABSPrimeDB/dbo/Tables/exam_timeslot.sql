CREATE TABLE [dbo].[exam_timeslot] (
    [timecode] SMALLINT NOT NULL,
    [row]      TINYINT  NOT NULL,
    [col]      TINYINT  NOT NULL,
    [briefing] TINYINT  NULL,
    [session]  TINYINT  NULL,
    [exam]     CHAR (1) NOT NULL,
    CONSTRAINT [PK_exam_timeslot] PRIMARY KEY CLUSTERED ([timecode] ASC, [row] ASC, [col] ASC, [exam] ASC) WITH (FILLFACTOR = 90)
);


GO
CREATE NONCLUSTERED INDEX [exam_timeslot_col]
    ON [dbo].[exam_timeslot]([col] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [exam_timeslot_timecode]
    ON [dbo].[exam_timeslot]([timecode] ASC) WITH (FILLFACTOR = 90);

