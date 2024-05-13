create proc get_track_all   
   @candidate char(6) 
as 

select  year, exam, type from track 
where trackno = @candidate order by year,exam DESC
