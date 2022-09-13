<Query Kind="Expression">
  <Connection>
    <ID>2e5c8bd4-742f-4f7f-8356-6517a86c5d29</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>LAPTOP-ID3EIIJL</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//sorting

//there is a significant difference between query syntax 
//and  method syntax.

//Query syntax is much like sql
//orderby field {[ascending]| descending} [,field....]

//ascending is the default option

//method syntax is a series of individual methods 
//.orderby(x=>x.field)--first field only
//.OrderByDescending(x=>x.field)---first field only
//.ThenBy(x=>x.field)--each following field
//.ThenByDescending(x=>x.field)--each following field.



//Find all of the albums tracks for the band Queen
//Order the tracks by the tracknames alphabetically.


//query syntax method
from x in Tracks 
    where x.Album.Artist.Name.Contains("Queen")
	orderby x.AlbumId, x.Name 
	 select x
	 
 //method syntax
 Tracks
 .Where (x=>x.Album.Artist.Name.Contains("Queen"))
 .OrderBy(x=> x.Album.Title)
 .ThenBy(x => x.Name)
 
 //order of sorting and filtering can be interchanged 
  Tracks
 .OrderBy(x=> x.Album.Title)
 .ThenBy(x => x.Name)
 .Where (x=>x.Album.Artist.Name.Contains("Queen"))
	
	