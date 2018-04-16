package core;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;

public class DB_Connection {

	static public String line = System.getProperty("line.separator");
	//DATBASE INFO VARIABLES
	public static String user = "root", pass = "", URL = "jdbc:mysql://localhost:3306/agenda database", name, year, month, day, hour, min, action;
	public static String status = "NULL";
	//MYSQL VARIABLES
	public static Connection DB_connection;
	public static Statement DB_statement;

	public static void DB_connect(){
		try{
		
			Connection DB_connection = DriverManager.getConnection( URL, user, pass);
		
			status = "TRUE";
		
		}catch(Exception e){ System.out.println("Error: " + e ); status = "FALSE";}return;}
	
	public static void fetch(){
		try{
			Connection DB_connection = DriverManager.getConnection( URL, "root", "");
			Statement DB_statement = DB_connection.createStatement();
			ResultSet DB_results = DB_statement.executeQuery("SELECT `name`, `year`, `month`, `day`, `hour`, `minute` FROM `time` WHERE 1");
			
			//GUI_Startup.info.append("Hello, here is your agenda...");
		
			while(DB_results.next()){
			
				//GUI_Startup.info.append(line + "Appoinment ~ Name: " + DB_results.getString("name") + line + DB_results.getString("month") + "," + DB_results.getString("day") + "," +DB_results.getString("year") + "," + DB_results.getString("hour") + ":" + DB_results.getString("minute"));
			
			}
		
		}catch(Exception e){System.out.println("Eror: " + e);}
		
		}
	
	public static void DB_ADD(){
		try{
			Connection DB_connection = DriverManager.getConnection(URL, "root", "");
			Statement DB_statement = DB_connection.createStatement();
		
			action = "INSERT INTO `time` (`name`, `year`, `month`, `day`, `hour`, `minute`) VALUES ('" + name + "', '" + year + "', '" + month + "', '" + day + "', '" + hour + "', '" + min + "');";
			System.out.println(action);
		
			DB_statement.executeUpdate(action);
			
		}catch(Exception e){System.out.println("Eror: " + e);}
		
		}
		
		public static void DB_REMOVE(){
		try{
			Connection DB_connection = DriverManager.getConnection(URL, "root", "");
			Statement DB_statement = DB_connection.createStatement();
		
			action = "DELETE FROM `time` WHERE `name` = " + name +  " AND `year` = " + year + " AND `month` = " + month + " AND`day` = " + day + " AND `hour` = " + hour + " AND `minute` = " + min;
			System.out.println(action);
		
			DB_statement.executeUpdate(action);
		
		}catch(Exception e){}}
		
}