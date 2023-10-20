package com.example.arhomedesign.utils;

import com.example.arhomedesign.LoginData;

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
    @GET("api/users/profile")
    Call<UserData> getProfile();
    @POST("api/login")
        //on below line we are creating a method to post our data.
    Call<LoginData> Login(@Body LoginData dataModal);
}
