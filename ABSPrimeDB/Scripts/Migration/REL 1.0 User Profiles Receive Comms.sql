UPDATE
    user_profiles
set
    ReceiveComms =  IIF(surgeon_st.st_val='Yes',1,0)
FROM
    user_profiles
INNER JOIN surgeon_st  ON
    user_profiles.ABSId = surgeon_st.candidate AND surgeon_st.status_code='UC'
