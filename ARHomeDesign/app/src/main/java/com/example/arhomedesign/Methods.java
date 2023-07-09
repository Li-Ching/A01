package com.example.arhomedesign;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface Methods {
    @GET("api/furnitures/1")
    Call<furnitures> getFurniture();
    @GET("api/furnitures")
    Call<List<furnitures>> getFurnitures();
}
