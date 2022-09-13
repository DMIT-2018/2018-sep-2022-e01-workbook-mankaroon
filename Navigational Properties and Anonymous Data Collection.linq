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

//navigation properties and anonymous data set (collection)

//reference: Student Notes/Demo/eRestaurant/Linq: Query and Method Syntax

//Find all albums released in the 90's (1990-1999)

//Order the albums in ascending year and then alphabetically by album title

//display the year, title, artist, name and release label.

//concerns: a) not all properties of album are to be displayed
           // b) the order of the property are to be displayed in a 
		   //different sequence then the definition of the properties of 
		   // the properties on the entity Album.
		   //c) the artist name is not on the album table but is on
		   //the artist table.
		   
//Solution: use an anonymous data collectiom
//the anonymous data instance is defined within the select by 
//declared fields (properties)
//the order of the fields on the new defined instance will be 
//done in specifying the properies of the anonymous dara collection
//

//method instance
Albums
  .Where(x => x.ReleaseYear > 1989 && x.ReleaseYear < 2000)
  .OrderBy (x=> x.ReleaseYear) 
  .ThenBy(x=> x.Title)
  .Select(x=> new
  
      {
	  Year = x.ReleaseYear,
	  Title = x.Title,
	  Artist = x.Artist.Name,
	  Label =x.ReleaseLabel
  
       })
//	    .OrderBy (x=> x.Year) //Year is in the anonymous data type definition,Release Year is not.
//    .ThenBy(x=> x.Title)
	   