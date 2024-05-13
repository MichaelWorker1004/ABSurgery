CREATE FUNCTION udf_get_certificate_number
(
  @candidate VARCHAR(6),
  @exam      VARCHAR(1)
)
RETURNS VARCHAR(6) AS
BEGIN
  DECLARE @result VARCHAR(6)
  BEGIN
    SELECT
      @result=MAX(certificate)
    FROM
      exam_pass
    WHERE
      candidate                  = @candidate
    AND exam                     = @exam
    AND ISNULL(certificate, '') <> ''
    GROUP BY
      candidate,
      exam
  END
  
  RETURN @result
END