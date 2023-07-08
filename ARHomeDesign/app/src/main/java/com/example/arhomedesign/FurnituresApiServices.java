package com.example.arhomedesign;

import retrofit2.Call;
import retrofit2.http.GET;

// This interface defines an API
// service for getting furnitures.
public interface FurnituresApiServices {
    // This annotation specifies that the HTTP method
    // is GET and the endpoint URL is "api/furnitures".
    @GET("1")

    // This method returns a Call object with a generic
    // type of Furnitures, which represents
    // the data model for the response.
    Call<Furnitures> getFurnitures();
}
