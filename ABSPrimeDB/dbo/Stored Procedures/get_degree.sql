create proc get_degree
as
    select 
        description as ItemDisplay,
        degree_id as ItemValue
    from 
        degree
