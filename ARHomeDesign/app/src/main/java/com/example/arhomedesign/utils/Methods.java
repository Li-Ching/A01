package com.example.arhomedesign.utils;

import com.example.arhomedesign.LoginData;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

public interface Methods {
    @GET("api/furnitures/1")
    Call<furnitures> getFurniture1();
    @GET("api/furnitures/2")
    Call<furnitures> getFurniture2();
    @GET("api/furnitures/3")
    Call<furnitures> getFurniture3();
    @GET("api/furnitures")
    Call<List<furnitures>> getFurnitures();

}
