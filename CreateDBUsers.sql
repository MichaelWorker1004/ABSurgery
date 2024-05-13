use master
--create login [jshen@absurgery.org] from external provider
--create login [tfairbrother@absurgery.org] from external provider
--create login [ldriscoll@absurgery.org] from external provider

--create login [oigbokwe@absurgery.org] from external provider

--create a role for executing stored procedures and grant this role permissions to do that
--CREATE ROLE db_spexecutor
--Grant execute to db_spexecutor

--declare variables
declare @target_db_user as nvarchar(100)

declare @loop_count int
declare @loop_index int = 0
declare @user_list table(item_index int, upn nvarchar(100))

--populate user list with data
insert into @user_list values 
(0,'oigbokwe'), 
(1,'jshen'),
(2,'tfairbrother'),
(3,'ldriscoll')

--set loop count
select @loop_count = count(*) from @user_list

--loop through all users
while @loop_index < @loop_count
begin
	--set the current user (from data that was populated)
	select @target_db_user = upn from @user_list where item_index = @loop_index
	declare @cmd_create nvarchar(200) = 'CREATE USER [' + @target_db_user +'@absurgery.org] FROM EXTERNAL PROVIDER;'
	declare @cmd_alterrole_reader nvarchar(200) = 'ALTER ROLE db_datareader ADD MEMBER [' + @target_db_user + '@absurgery.org];'
	declare @cmd_alterrole_writer nvarchar(200) = 'ALTER ROLE db_datawriter ADD MEMBER [' + @target_db_user + '@absurgery.org];'
	declare @cmd_alterrole_executor nvarchar(200) = 'ALTER ROLE db_spexecutor ADD MEMBER [' + @target_db_user + '@absurgery.org];'

	select @cmd_create

	--create the user
	exec (@cmd_create);
	exec (@cmd_alterrole_reader);
	exec (@cmd_alterrole_writer);
	exec (@cmd_alterrole_executor);

	--increment the loop
	set @loop_index = @loop_index + 1
end


