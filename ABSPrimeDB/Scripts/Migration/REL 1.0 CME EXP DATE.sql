UPDATE CME
SET CME.credit_exp_date = DATEADD(year,5,CME.ReleaseDate)
WHERE CME.credit_exp_date IS NULL