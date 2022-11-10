<Query Kind="Program">
  <Connection>
    <ID>eda45876-5cfe-48bf-940f-d29ff9052bc6</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.</Server>
    <Database>GAMES</Database>
    <DisplayName>game-entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{



	//DISPLAYTEAMS.Dump();
	var list =GetGameData();
	Game_RecordGame(list);
	
	Display_result();
}

// You can define other methods, fields, classes and namespaces here


//create query models and command models
#region result

void Display_result()
{
	
	var rsts = DISPLAYTEAMS
	              
	

								.Select(x => new
								{
									Id = x.TEAMID,
									NAME = x.NAME,
									WINS =x.WINS,
									Looses =x.LOOSES
									
								})
								.Dump();





}
#endregion

#region queries
public class getTeams
{
public int TeamID {get;set;}
public string Name {get;set;}
public int? wins {get;set;}
public int? loose {get;set;}
}
public class DisplayGames
{
    public int? ID {get;set;}
	public DateTime? date { get; set; }
	public int? HomeTeamID { get; set; }
	public string homename { get; set; }
	public int? homescore { get; set; }
	public int? visitingteamID { get; set; }
	public string VisitingTeamName { get; set; }
	public int? visitingteamscore { get; set; }

}

#endregion

#region command
public class GetStat
{
	
	public DateTime date { get; set; }
	public int HomeTeamID { get; set; }
	public string homename{get;set;}
	public int homescore { get; set; }
	public int visitingteamID { get; set; }
	public string VisitingTeamName { get; set; }
	public int visitingteamscore { get; set; }
	
}
#endregion
#region testdata
List<GetStat> GetGameData()
{


	List<GetStat> GetGamesStat = new List<GetStat>();

	GetGamesStat.Add(new GetStat()
	{
		//date = new DateTime(11/9/2022),
		
		date = new DateTime(1/1/1900),
		HomeTeamID = 4,
		homename = "Orange",
		homescore = 6,
		visitingteamID = 1,
		VisitingTeamName = "Amazing",
		visitingteamscore = 14

	});



	return GetGamesStat.ToList();

}
#endregion

#region TrackServices
List<getTeams>FetchTeamData(){
	IEnumerable <getTeams> result = DISPLAYTEAMS
	                      .OrderBy(x=>x.NAME)
	                      .Select(x=> new getTeams
						  {
						  	TeamID =x.TEAMID,
							Name =x.NAME,
							wins =x.WINS,
							loose =x.LOOSES
		
						  });
						  return result.ToList();
}
List<DisplayGames> FetchGameData()
{
	IEnumerable<DisplayGames> result = GAMES
	                          .OrderBy(x=>x.ID)
	                              
						
						  .Select(x => new DisplayGames
						  {
						  	date =x.DATE,
							homename =x.HOMENAME,
							homescore =x.HOMESCORE,
							HomeTeamID =x.HOMETEAMID,
							visitingteamID =x.VISITINGTEAMID,
							VisitingTeamName =x.VISITINGNAME,
							visitingteamscore =x.VISITINGSCORE
						  

						  });
	return result.ToList();
}
#endregion

#region command trx
public void Game_RecordGame( List<GetStat>GetGamesStat)
{
	GAMES gameexist = null;
	DISPLAYTEAMS Hometeamsexist =null;
	DISPLAYTEAMS Visitingteamsexist =null;
	
	
	
	List<Exception> errorlist = new List<Exception>();
	foreach(GetStat item in GetGamesStat)
	{
	
		 Hometeamsexist = DISPLAYTEAMS
								.Where(x =>x.TEAMID==item.HomeTeamID)
								.FirstOrDefault();
		Visitingteamsexist = DISPLAYTEAMS
								.Where(x => x.TEAMID == item.visitingteamID)
								.FirstOrDefault();
								


		if(item.HomeTeamID==0)
		{
			errorlist.Add(new Exception("you must select a hometeamID"));
		}
		if (item.visitingteamID == 0)
		{
			errorlist.Add(new Exception("you must select a visitingteamID"));
		}
		//if (item.date !=DateTime.Now)
		//{
		//	errorlist.Add(new Exception("you cannot select a future date or a previous date"));
		//}
		if(item.HomeTeamID==item.visitingteamID)
		{
			errorlist.Add(new Exception("a team cannot play by itself"));
		}
		if(item.visitingteamscore==item.homescore)
		{
			errorlist.Add(new Exception("score cannot be tied"));
		}
		
		gameexist = GAMES
		            .Where(x=> x.DATE==item.date
					&&x.VISITINGTEAMID==item.visitingteamID
					&&x.HOMETEAMID==item.HomeTeamID
					&&x.HOMESCORE==item.homescore
					&&x.VISITINGSCORE==item.visitingteamscore)
					.FirstOrDefault();
					
		
		if(gameexist!=null)
		{
			errorlist.Add(new Exception("the database already contains the teams score on this date"));
		}

		//getting a list of games played by the visiting team


		if(gameexist==null)
		{

			gameexist = new GAMES()

			{
				DATE = item.date,
		
			HOMESCORE = item.homescore,
			VISITINGNAME = item.VisitingTeamName,
			VISITINGSCORE = item.visitingteamscore
		  };
			

			if (item.visitingteamscore > item.homescore)
			{

              Visitingteamsexist.WINS++;
			  
			 

			}
			
			if (item.visitingteamscore < item.homescore)
			{
				
				Visitingteamsexist.LOOSES++;
				
				

			}
			if (item.homescore > item.visitingteamscore)
			{

				Hometeamsexist.WINS++;
				

			}

			if (item.homescore < item.visitingteamscore)
			{

				Hometeamsexist.LOOSES++;

			}

			DISPLAYTEAMS.Update(Hometeamsexist);
			DISPLAYTEAMS.Update(Visitingteamsexist);


		}
		else{

			var rs = GAMES
						.Where(x => x.DATE == item.date
						&& x.VISITINGTEAMID == item.visitingteamID
						&& x.HOMETEAMID == item.HomeTeamID
						&& x.HOMESCORE == item.homescore
						&& x.VISITINGSCORE == item.visitingteamscore)
						.SingleOrDefault();
			errorlist.Add(new Exception($"the database already contains{rs} the teams score on this date"));
		}



	




	}
		

	 
if (errorlist.Count > 0)
{
	throw new AggregateException("unable to add skills to employee", errorlist);
}
else
{
	//all work has been staged
	SaveChanges();
}

	}

#endregion