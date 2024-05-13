CREATE TABLE [dbo].[fee_received] (
    [id]             NUMERIC (9)   IDENTITY (1, 1) NOT NULL,
    [UserId]        INT           NULL,
    [ref_id]         NUMERIC (9)   NULL,
    [type]           CHAR (1)      NULL,
    [name]           VARCHAR (100) NULL,
    [invoice]        VARCHAR (16)  NULL,
    [inv_num]        VARCHAR (16)  NULL,
    [amount]         MONEY         NULL,
    [check_num]      VARCHAR (20)  NULL,
    [trans_id]       VARCHAR (46)  NULL,
    [trans_time]     DATETIME      NULL,
    [trans_log]      TEXT          NULL,
    [date_deposited] DATETIME      NULL,
    [acct_deposited] VARCHAR (5)   NULL,
    CONSTRAINT [PK_fee_received] PRIMARY KEY NONCLUSTERED ([id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_fee_received_users] FOREIGN KEY ([UserId]) REFERENCES [users]([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_fee_received_inv_num]
    ON [dbo].[fee_received]([inv_num] ASC) WITH (FILLFACTOR = 90);


GO
CREATE TRIGGER tr_fee_received_insert ON dbo.fee_received
AFTER INSERT
AS

DECLARE @inv_num varchar(16), @discount_code varchar(8)
/****DISABLE THIS TRIGGER****************

SELECT @inv_num=inv_num FROM inserted

IF RIGHT(@inv_num,3)='R0E' AND CHARINDEX('ZR', @inv_num)=0
BEGIN
	--inserting discount 'DCCW' or 'DCCR'
	SELECT @discount_code=
		ISNULL(CAST(CASE 
			WHEN RIGHT(@inv_num,3)='R0E' AND 
				cca_discount_applied.inv_num IS NULL AND 
				ISNULL(cca_fee_amount,0)>0 AND 
				ISNULL(dbo.udf_get_balance(@inv_num),0)>0 AND 				
				(ISNULL(dbo.udf_get_academic_year('M')-YEAR(surgeon_st.start_date)+1,0) IN (2,4) OR (examrepeater.candidate IS NOT NULL AND surgeon_st.start_date IS NULL)) 
				AND LF.candidate IS NULL
			THEN 'DCCR'		
		
			/*WHEN RIGHT(@inv_num,3)='R0E' AND 
				cca_discount_applied.inv_num IS NULL AND 
				cca_fee_amount>0 AND 
				ISNULL(dbo.udf_get_balance(@inv_num),0)-cca_fee_amount>=0 AND 
				ISNULL(dbo.udf_get_academic_year('M')-YEAR(surgeon_st.start_date)+1,0) NOT IN (2,4)
			THEN 'DCCW'*/

			ELSE ''
		END AS varchar),'')
	FROM surgeon 
		OUTER APPLY dbo.udf_get_cc_req(surgeon.candidate) cc_req 
	    LEFT JOIN 
			(SELECT max(inv_num) inv_num FROM inv_det WHERE code IN ('DCCW','DCCR') AND amount*quantity>0 AND CHARINDEX('R',inv_num)>0 GROUP BY inv_num) cca_discount_applied ON LEFT(cca_discount_applied.inv_num,10)=LEFT(@inv_num,10) 	
	    
		LEFT JOIN surgeon_st ON surgeon_st.candidate=surgeon.candidate AND surgeon_st.status_code='LP' AND cc_status=7 
		LEFT JOIN surgeon_st LF ON LF.candidate=surgeon.candidate AND LF.status_code='LF'
		LEFT JOIN (select candidate,MAX(year) year FROM exam WHERE status='C' GROUP BY candidate) examrepeater ON examrepeater.candidate=surgeon.candidate AND examrepeater.year=dbo.udf_get_academic_year('M')-1 
	    LEFT JOIN (SELECT inv_num, MAX(IIF(CHARINDEX('CCAF', code)>0,amount*quantity,0)) cca_fee_amount FROM inv_det GROUP BY inv_num) fee_owed ON fee_owed.inv_num=LEFT(@inv_num,10)+'MCA'
	WHERE surgeon.candidate=LEFT(@inv_num,6);
	
	IF (LEN(ISNULL(@discount_code,''))>0)
	BEGIN
		INSERT INTO inv_det (creator,inv_num,code,amount,quantity) 
			SELECT 'fee', z.inv_num, a.code, cast(isnull(a.attr,0) as money), 1 
			FROM invoice z
				INNER JOIN mcodes a ON a.code=@discount_code AND a.grp='FC' AND a.attr2='1'
				LEFT JOIN inv_det ON inv_det.inv_num=z.inv_num AND inv_det.code IN ('DSAT','DCCW','DCCR','DCCH') AND inv_det.amount*inv_det.quantity!=0
			WHERE 
				z.inv_num=@inv_num AND inv_det.inv_num IS NULL
	END
	
		INSERT INTO inv_det (creator,inv_num,code,amount,quantity) 
		SELECT 'fee', z.inv_num, a.code, cast(isnull(a.attr,0) as money), 1 
		FROM invoice z	
			INNER JOIN mcodes a ON a.code='DSAT' AND a.grp='FC' AND a.attr2='1' 
			LEFT JOIN inv_det dsat_exists ON LEFT(dsat_exists.inv_num,10)=LEFT(@inv_num,10) AND dsat_exists.code='DSAT' AND dsat_exists.amount*dsat_exists.quantity!=0
			WHERE 
				dsat_exists.inv_num IS NULL AND 
				z.inv_num!=@inv_num AND 
				LEFT(z.inv_num,10)=LEFT(@inv_num,10) AND RIGHT(z.inv_num,3)='R0E' AND 
				z.inv_num NOT IN 
					(SELECT inv_num FROM inv_det WHERE code IN ('DSAT','DCCW','DCCR','DCCH') AND LEFT(inv_num,10)=LEFT(@inv_num,10) AND RIGHT(inv_num,3)='R0E' AND amount*quantity!=0);	
END

*/
if @@error != 0
begin 
	RAISERROR('Unable to update table', 16, 1)
end
GO
DISABLE TRIGGER [dbo].[tr_fee_received_insert]
    ON [dbo].[fee_received];

