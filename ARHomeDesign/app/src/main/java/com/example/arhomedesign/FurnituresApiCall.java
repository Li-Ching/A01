package com.example.arhomedesign;

import android.content.Context;
import android.widget.Toast;

import androidx.fragment.app.Fragment;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class FurnituresApiCall {
    // This function takes a Context and callback function
    // as a parameter, which will be called
    // when the API response is received.
    public void getFurnitures(Fragment fragment, final Callback<Furnitures> callback) {
        // Use the fragment as the context parameter
        Context context = fragment.requireContext();

        // Create a Retrofit instance with the base URL and
        // a GsonConverterFactory for parsing the response.
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("http://140.137.41.136:1380/A01/api/furnitures/")
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        // Create an ApiService instance from the Retrofit instance.
        FurnituresApiServices service = retrofit.create(FurnituresApiServices.class);

        // Call the getFurnitures() method of the ApiService
        // to make an API request.
        Call<Furnitures> call = service.getFurnitures();

        // Use the enqueue() method of the Call object to
        // make an asynchronous API request.
        call.enqueue(new Callback<Furnitures>() {
            // This is an anonymous inner class that implements the Callback interface.
            @Override
            public void onResponse(Call<Furnitures> call, Response<Furnitures> response) {
                // This method is called when the API response is received successfully.

                if (response.isSuccessful()) {
                    // If the response is successful, parse the
                    // response body to a DataModel object.
                    Furnitures furnitures = response.body();

                    // Call the callback function with the DataModel
                    // object as a parameter.
                    callback.onResponse(call, Response.success(furnitures));
                }
            }

            @Override
            public void onFailure(Call<Furnitures> call, Throwable t) {
                // This method is called when the API request fails.
                Toast.makeText(context, "Request Fail", Toast.LENGTH_SHORT).show();
                callback.onFailure(call, t);
            }
        });
    }
}
