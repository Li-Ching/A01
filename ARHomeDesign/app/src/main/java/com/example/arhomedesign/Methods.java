package com.example.arhomedesign;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

public interface Methods {
    @GET("api/furnitures/1")
    Call<furnitures> getFurniture();
    @GET("api/furnitures")
    Call<List<furnitures>> getFurnitures();
    @POST("api/users")
        //on below line we are creating a method to post our data.
    Call<UserData> createPost(@Body UserData dataModal);
}
