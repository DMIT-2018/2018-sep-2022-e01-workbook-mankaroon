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

//List all albums by release label. Any album with no label 
//should be indicated as unknown 
//list Title, label and Artist name
//OrderbY release label
//order by the release label field

//understand the problem
    //collection: albums
	//selective data: anonymous data set 
	// label (nullabe):either unknown or label name ****
	
	//design 
	//albums 
	//select (new{{})
	//fields :title
	         //Label?? ternary operator (condition(s) ? true value : false value)
			 //Artist.Name
  //coding and testing
  
  Albums 
 // .OrderBy(x=> x.ReleaseLabel)
  .Select(x=>new 
  
  {
  
  Title = x.Title,
  Label = x.ReleaseLabel == null ? "unknow" : x.ReleaseLabel,
  Artist = x.Artist.Name
  }
 
  )
  .OrderBy(x=> x.Label).
  
  //list all albums showing the title, artist, name , Year and decade of 
  //release using  oldies, 70s, 80s, 90s, or modern.
  //Order by decade
  
  Albums 
    .Select (x => new 
	 {
	 
	   
				  Title = x.Title,
				  Artist = x.Artist.Name,
				   Year = x.ReleaseYear,
				   Decade = x.ReleaseYear < 1970? "Oldies":
				           x.ReleaseYear < 1980 ?  "70s":
						   x.ReleaseYear < 1990? "80s":
				           x.ReleaseYear < 2000 ?  "90s": "Modern"
				           
		
					
	
	 }
	
	)
	.OrderBy(x => x.Year)
  
  
  