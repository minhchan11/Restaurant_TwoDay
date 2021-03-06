using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Yelp
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _favDish;
    private DateTime _date;
    private int _cuisineId;

    public Restaurant(string Name, string FavDish, DateTime Date, int CuisineId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _favDish = FavDish;
      _date = Date;
      _cuisineId = CuisineId;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool nameEquality = (this.GetName() == newRestaurant.GetName());
        bool favDishEquality = (this.GetFavDish() == newRestaurant.GetFavDish());
        bool dateEquality = (this.GetDate() == newRestaurant.GetDate());
        bool cuisineIdEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        return (idEquality && nameEquality && favDishEquality && dateEquality && cuisineIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetFavDish()
    {
      return _favDish;
    }
    public void SetFavDish(string newFavDish)
    {
      _favDish = newFavDish;
    }

    public DateTime GetDate()
    {
      return _date;
    }
    public void SetDate(DateTime newDate)
    {
      _date = newDate;
    }

    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public void SetCuisineId(int newCuisineId)
    {
      _cuisineId = newCuisineId;
    }

    public static List<Restaurant> GetAll()
     {
       List<Restaurant> allRestaurants = new List<Restaurant>{};

       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants ORDER BY cast([opening_date] as datetime) asc;", conn);
       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         int restaurantId = rdr.GetInt32(0);
         string restaurantName = rdr.GetString(1);
         string favDish = rdr.GetString(2);
         DateTime openingDate = rdr.GetDateTime(3);
         int cuisineId = rdr.GetInt32(4);
         Restaurant newRestaurant = new Restaurant(restaurantName, favDish, openingDate, cuisineId, restaurantId);
         allRestaurants.Add(newRestaurant);
       }

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }

       return allRestaurants;
     }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, fav_dish, opening_date, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantFavDish, @RestaurantDate, @RestaurantCuisineId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();

      SqlParameter favDishParameter = new SqlParameter();
      favDishParameter.ParameterName = "@RestaurantFavDish";
      favDishParameter.Value = this.GetFavDish();

      SqlParameter dateParameter = new SqlParameter();
      dateParameter.ParameterName = "@RestaurantDate";
      dateParameter.Value = this.GetDate();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(favDishParameter);
      cmd.Parameters.Add(dateParameter);
      cmd.Parameters.Add(cuisineIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        //id is the only private property left to be assigned to the instance of the class
        this._id = rdr.GetInt32(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@RestaurantId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;
      string foundDish = null;
      DateTime foundDate = new DateTime();
      int foundCuisineId = 0;

      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundDish = rdr.GetString(2);
        foundDate = rdr.GetDateTime(3);
        foundCuisineId = rdr.GetInt32(4);
      }

      Restaurant foundRestaurant = new Restaurant(foundName,foundDish,foundDate,foundCuisineId, foundId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundRestaurant;
    }

    public void UpdateName(string newName)
    {
      if (newName != "")
      {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET name = @NewName OUTPUT INSERTED.name WHERE id=@RestaurantId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value= newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value= this.GetId();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
      }
    }

    public void UpdateFavDish(string newFavDish)
    {
      if (newFavDish != "")
      {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET fav_dish = @NewFavDish OUTPUT INSERTED.fav_dish WHERE id=@RestaurantId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewFavDish";
      newNameParameter.Value = newFavDish;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value= this.GetId();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._favDish = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
      }
    }

    public void UpdateDate(DateTime newDate)
    {
      DateTime defaultDate = new DateTime(1800,1,1);
      if (newDate != defaultDate)
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE restaurants SET opening_date = @NewDate OUTPUT INSERTED.opening_date WHERE id=@RestaurantId;", conn);

        SqlParameter newDateParameter = new SqlParameter();
        newDateParameter.ParameterName = "@NewDate";
        newDateParameter.Value= newDate;
        cmd.Parameters.Add(newDateParameter);

        SqlParameter restaurantIdParameter = new SqlParameter();
        restaurantIdParameter.ParameterName = "@RestaurantId";
        restaurantIdParameter.Value= this.GetId();
        cmd.Parameters.Add(restaurantIdParameter);
        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._date = rdr.GetDateTime(0);
        }

        if (rdr != null)
        {
          rdr.Close();
        }

        if (conn != null)
        {
          conn.Close();
        }
      }
    }

    public void UpdateCuisineId(int newCuisineId)
    {
      if (newCuisineId != 0)
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE restaurants SET cuisine_id = @NewCuisineId OUTPUT INSERTED.cuisine_id WHERE id=@RestaurantId;", conn);

        SqlParameter newCuisineIdParameter = new SqlParameter();
        newCuisineIdParameter.ParameterName = "@NewCuisineId";
        newCuisineIdParameter.Value= newCuisineId.ToString();
        cmd.Parameters.Add(newCuisineIdParameter);

        SqlParameter restaurantIdParameter = new SqlParameter();
        restaurantIdParameter.ParameterName = "@RestaurantId";
        restaurantIdParameter.Value= this.GetId();
        cmd.Parameters.Add(restaurantIdParameter);
        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._cuisineId = rdr.GetInt32(0);
        }

        if (rdr != null)
        {
          rdr.Close();
        }

        if (conn != null)
        {
          conn.Close();
        }
      }
    }

    public List<Review> GetRestaurantReview()
    {
      List<Review> allReviews = new List<Review>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE restaurant_id = @RestaurantId;", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(restaurantIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string review = rdr.GetString(1);
        int rating = rdr.GetInt32(2);
        int restaurantId = rdr.GetInt32(3);
        Review newReview = new Review(review, rating, restaurantId, id);
        allReviews.Add(newReview);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReviews;
    }

    // public static List<Restaurant> GetRestaurantMinReview()
    // {
    //   List<Restaurant> allRestaurants = new List<Restaurant>{};
    //
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT restaurant_id FROM reviews WHERE rating >= @Rating;", conn);
    //
    //   SqlParameter minRatingParameter = new SqlParameter();
    //   minRatingParameter.ParameterName = "@Rating";
    //   minRatingParameter.Value = "3";
    //   cmd.Parameters.Add(minRatingParameter);
    //
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   while(rdr.Read())
    //   {
    //     int id = rdr.GetInt32(0);
    //
    //     SqlCommand cmd2 = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
    //
    //     SqlParameter restaurantIdParameter = new SqlParameter();
    //     restaurantIdParameter.ParameterName = "@RestaurantId";
    //     restaurantIdParameter.Value = id;
    //     cmd.Parameters.Add(restaurantIdParameter);
    //
    //     SqlDataReader rdr2 = cmd.ExecuteReader();
    //
    //     while(rdr2.Read())
    //     {
    //       int restaurantId = rdr2.GetInt32(0);
    //       string restaurantName = rdr2.GetString(1);
    //       string favDish = rdr2.GetString(2);
    //       DateTime openingDate = rdr2.GetDateTime(3);
    //       int cuisineId = rdr2.GetInt32(4);
    //       Restaurant newRestaurant = new Restaurant(restaurantName, favDish, openingDate, cuisineId, restaurantId);
    //       allRestaurants.Add(newRestaurant);
    //     }
    //     if (rdr2 != null)
    //     {
    //       rdr2.Close();
    //     }
    //   }
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return allRestaurants;
    // }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }

    public void DeleteThisRestaurant()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @RestaurantId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@RestaurantId";
      idParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

  }
}
