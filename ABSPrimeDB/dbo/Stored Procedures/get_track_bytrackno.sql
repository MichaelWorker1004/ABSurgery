
CREATE proc get_track_bytrackno   
   @exam char(1),
   @type char(1),
   @year smallint,
   @candidate char(6) 
as 

select  convert(char(8),pre_send,1), convert(char(8),pre_receive,1),
	convert(char(8),pre_approve,1), convert(char(8),pre_disapprove,1),
	convert(char(8),pre_defer,1), convert(char(8),pre_credential,1),
	convert(char(8),app_send,1), convert(char(8),app_receive,1),
	convert(char(8),app_approve,1), convert(char(8),app_disapprove,1),
	convert(char(8),app_defer,1), convert(char(8),app_credential,1),
	convert(char(8),oper_receive,1), convert(char(8),oper_approve,1),
	convert(char(8),card_receive,1), convert(char(8),lic_receive,1),
	convert(char(8),reg_receive,1), convert(char(8),let_send,1), 
	convert(char(8),let_receive,1), convert(char(8),fee_amount,1),
	convert(char(8),fee_receive,1), convert(char(8),fee_bounce,1),
	track.note,
	convert(char(8),sig_receive,1),  convert(char(8),cme_receive,1),isnull(trans_id ,''),
	isnull(a.status,0),isnull(b.status,0),web_note
from track 
left join app_response a on a.candidate=track.candidate and left(a.exam_type,6)=cast(track.year as varchar)+track.exam+track.type and a.app_id=0
left join app_response b on b.candidate=track.candidate and left(b.exam_type,6)=cast(track.year as varchar)+track.exam+track.type and b.app_id=1
where exam = @exam   and type = @type   and year = @year   and 
trackno = @candidate
