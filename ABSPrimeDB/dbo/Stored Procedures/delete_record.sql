CREATE proc delete_record
	@candidate char(6),
	@examcode varchar(7),
	@type char(1)
AS 
	IF (@type='E')
	BEGIN
		DELETE FROM inv_det WHERE inv_num =
		(
			select inv_num
			from exam
			left join invoice on invoice.ref_id=exam.id and invoice.type='E'
			where candidate=@candidate and examcode=@examcode
		)

		DELETE FROM invoice WHERE inv_num =
		(
			select inv_num
			from exam
			left join invoice on invoice.ref_id=exam.id and invoice.type='E'
			where candidate=@candidate and examcode=@examcode
		)

		DELETE FROM exam WHERE candidate=@candidate and examcode=@examcode
	END
	ELSE
	BEGIN
		DELETE FROM inv_det WHERE inv_num =
		(
			select inv_num
			from track
			left join invoice on invoice.ref_id=track.id and invoice.type='A'
			where candidate=@candidate and examcode=@examcode
		)

		DELETE FROM invoice WHERE inv_num =
		(
			select inv_num
			from track
			left join invoice on invoice.ref_id=track.id and invoice.type='A'
			where candidate=@candidate and examcode=@examcode
		)

		DELETE FROM track WHERE candidate=@candidate and examcode=@examcode
	END