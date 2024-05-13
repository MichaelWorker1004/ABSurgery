CREATE function dbo.udf_get_opex(@candidate char(6),@exam_type varchar(6))
RETURNS @opex TABLE (begindate varchar(10),
				enddate varchar(10),
				opproc float,
				is_cert varchar(6),
				CertificationDuration varchar(12),
				certificate_expiration varchar(10),
				CertificationExpireDate varchar(10),
				oper_last_updt smalldatetime,
				approved smalldatetime,
				isvalid tinyint,
				explain varchar(8000),
				lp_enddate smalldatetime,
				islf tinyint
			)
AS

BEGIN
	DECLARE @x xml,@oproc float,@begindate varchar(10), @enddate varchar(10), @explain varchar(8000), @oper_last_updt smalldatetime, @approved smalldatetime, @id numeric;
	
	IF LEN(@exam_type)=1
	BEGIN
		SELECT @id=MAX(id) FROM app_response WHERE candidate=@candidate AND app_id=1 AND CHARINDEX(@exam_type,exam_type)>0;
	END
	ELSE
	BEGIN
		SELECT @id=MAX(id) FROM app_response WHERE candidate=@candidate AND app_id=1 AND exam_type=@exam_type;
	END
	
	SELECT @x = app_response.response, @oper_last_updt = app_response.last_updt, @approved = ISNULL(app_response.approved,track.oper_approve)
	FROM app_response 
		LEFT JOIN track ON track.candidate=app_response.candidate AND track.examcode=app_response.exam_type
	WHERE app_response.id=@id;
	
	SELECT @begindate=ISNULL(@x.query('//q19').value('.','varchar(10)'),'');
	SELECT @enddate=ISNULL(@x.query('//q20').value('.','varchar(10)'),'');
	
	SELECT @oproc=ISNULL(@x.value('sum(//*[@oid])+sum(//q41[count(@oid)=0])+sum(//q42[count(@oid)=0])+sum(//q43[count(@oid)=0])+sum(//q44[count(@oid)=0])+sum(//q106[count(@oid)=0])+sum(//q107[count(@oid)=0])+sum(//q108[count(@oid)=0])+sum(//q109[count(@oid)=0])+sum(//q112[count(@oid)=0])+sum(//q113[count(@oid)=0])+sum(//q114[count(@oid)=0])+sum(//q182[count(@oid)=0])','float'),0);
	IF CHARINDEX('C', @exam_type)>0 
	BEGIN
		SET @oproc=@oproc+
			ISNULL(@x.value('count(//q134)','float'),0)+
			ISNULL(@x.value('count(//q143)','float'),0)+
			(SELECT count(*) FROM caselog WHERE candidate=@candidate AND (dbo.udf_get_academic_year('T')-year(created))<11)
	END
	
	SELECT @explain=ISNULL(@x.query('//other').value('.','varchar(8000)'),'');

	INSERT INTO @opex
	SELECT 
		@begindate begindate,
		@enddate enddate,
		@oproc opproc,
		ISNULL(v_abms_cert.certificate,'') is_cert,
		ISNULL(CertificationDuration,'') CertificationDuration,
		ISNULL(IIF(CertificationDuration='Continuous' AND surgeon_st.candidate IS NULL AND YEAR(v_abms_cert.CertificationExpireDate)>YEAR(getdate()), CONVERT(varchar(10),DATEADD(year, -1, v_abms_cert.CertificationExpireDate),120) ,CAST(v_abms_cert.CertificationExpireDate AS varchar(10))),'') certificate_expiration,
		ISNULL(CAST(v_abms_cert.CertificationExpireDate AS varchar(10)),'') CertificationExpireDate,
		ISNULL(@oper_last_updt,ISNULL(@approved,oper_receive)),
		ISNULL(@approved,oper_receive),
		IIF((@oproc>0 OR ISNULL(@approved,oper_receive) IS NOT NULL) AND (cast(mcodes.attr as int)-year(IIF(ISNULL(@enddate,'')='',ISNULL(@approved,oper_receive),@enddate))<11),1,0) isvalid,
		@explain explain,
		surgeon_st.end_date lp_enddate,
		IIF(ISNULL(lf.candidate,0)=0,0,1) islf 
	FROM  mcodes 
		LEFT JOIN v_abms_cert ON v_abms_cert.candidate=@candidate AND v_abms_cert.exam=LEFT(@exam_type,1) AND v_abms_cert.CertificationOccurrence!='FPD'
		LEFT JOIN surgeon_st ON surgeon_st.candidate=@candidate AND surgeon_st.status_code in ('LP'+LEFT(@exam_type,1),'LPCC'+LEFT(@exam_type,1)) AND surgeon_st.end_date>getdate()
		LEFT JOIN surgeon_st lf ON lf.candidate=@candidate AND lf.status_code='LF'+LEFT(@exam_type,1)
		LEFT JOIN (SELECT MAX(oper_receive) oper_receive FROM track WHERE candidate=@candidate AND exam=@exam_type AND type!='R' AND dbo.udf_get_academic_year('T')-year<11) oper_receive ON 1=1
	WHERE mcodes.grp='AY' and code='T' 

	RETURN
END