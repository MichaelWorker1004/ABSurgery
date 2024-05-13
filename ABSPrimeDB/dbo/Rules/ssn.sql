CREATE RULE [dbo].[ssn]
    AS @ssn like 'ABS[0-9]' or @ssn like '[0-9]';

