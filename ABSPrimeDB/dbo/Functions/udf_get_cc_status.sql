CREATE function udf_get_cc_status(@candidate char(6),@examcode varchar(6))

RETURNS TABLE
AS

RETURN (
        select
            track.examcode,
            track.candidate,
            trackid=track.id,
            track.app_receive,
            track.app_approve,
            track.pre_approve,
            isanyopexvalid,
            case track.type 
                when 'R' then cast(isnull(exam_fee_owed.fee,0)-isnull(exam_fee_received.paid,0) as varchar)
                else cast(isnull(fee_owed.fee,0)-isnull(fee_received.paid,0) as varchar) end fee_balance,
            track.sig_receive, 
            LicenseExpireDate,
            LEN(RTRIM(LTRIM(ISNULL(surgeon_st.note,'')))) status_note_len,
            left(dbo.udf_get_exam_eligibility(track.candidate,track.exam+dbo.udf_type_te(track.exam,track.type)),1) admissible,
            aal.candidate aal,
            ad.Descr ad,
            isnull(cc_moc_req_met,'Y') cc_moc_req_met,
            cc_status,
            cc_refletinstancesreq,
            cc_refletreceivedinstances,
            cc_refletsentinstances,
            cc_refletadverse,
            cc_refletnotapproved,
            cc_refletexpired,
            isnull(num_cycles_non_compliant,0) num_cycles_non_compliant
        from track 
            left join invoice on invoice.ref_id=track.id and invoice.type='A' 
            left join 
                (select inv_num,max(trans_time) fee_receive,sum(amount) paid from fee_received group by inv_num) fee_received on invoice.inv_num=fee_received.inv_num 
            left join 
                (select inv_num,sum(amount*quantity) fee from inv_det group by inv_num) fee_owed on invoice.inv_num=fee_owed.inv_num 
            
            left join dbo.udf_get_cc_req(@candidate) get_cc_req on get_cc_req.candidate=track.candidate
            
            left join exam on exam.candidate=track.candidate and track.examcode=exam.trackcode 
            
            left join (select max(LicenseExpireDate) LicenseExpireDate,candidate from licenses where LicenseTypeCode='FULL' and VerifyingOrganization!='SELF' group by candidate) license on license.candidate=track.candidate 
                    and LicenseExpireDate>=getdate()-1
            
            left join invoice exam_invoice on exam_invoice.ref_id=exam.id and exam_invoice.type='E' 
               left join 
                (select inv_num,max(trans_time) fee_receive,sum(amount) paid from fee_received group by inv_num) exam_fee_received on exam_invoice.inv_num=exam_fee_received.inv_num 
            left join 
                (select inv_num,sum(amount*quantity) fee from inv_det group by inv_num) exam_fee_owed on exam_invoice.inv_num=exam_fee_owed.inv_num 
            left join surgeon_st on surgeon_st.candidate=track.candidate and surgeon_st.status_code='CA'+track.examcode
            left join surgeon_st aal on aal.candidate=track.candidate and aal.status_code='AAL'
            left join mcodes ad on ad.code=track.examcode and ad.grp='AD'

	 where track.candidate=@candidate and track.examcode=@examcode

)