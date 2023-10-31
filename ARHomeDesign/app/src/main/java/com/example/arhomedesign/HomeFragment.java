package com.example.arhomedesign;

import android.content.Intent;
import android.os.Bundle;

import androidx.cardview.widget.CardView;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.LinearLayout;

import com.example.arhomedesign.utils.Methods;
import com.example.arhomedesign.utils.RetrofitClient;
import com.example.arhomedesign.utils.furnitures;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class HomeFragment extends Fragment {
    CardView recommend1, recommend2, recommend3;
    // Variables to hold recommended furniture data
    private furnitures recommendedFurniture1, recommendedFurniture2, recommendedFurniture3;


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        recommend1 = view.findViewById(R.id.butRec1);
        recommend2 = view.findViewById(R.id.butRec2);
        recommend3 = view.findViewById(R.id.butRec3);

        // Fetch recommended furniture data
        fetchRecommendedFurnitureData();

        // Set click listeners for CardView elements
        recommend1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                openFurnitureDetailsActivity(recommendedFurniture1);
            }
        });

        recommend2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                openFurnitureDetailsActivity(recommendedFurniture2);
            }
        });

        recommend3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                openFurnitureDetailsActivity(recommendedFurniture3);
            }
        });

        return view;
    }

    private void fetchRecommendedFurnitureData() {
        Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);

        Call<furnitures> call1 = methods.getFurniture1();
        Call<furnitures> call2 = methods.getFurniture2();
        Call<furnitures> call3 = methods.getFurniture3();

        call1.enqueue(new Callback<furnitures>() {
            @Override
            public void onResponse(Call<furnitures> call, Response<furnitures> response) {
                if (response.isSuccessful()) {
                    recommendedFurniture1 = response.body();
                }
            }

            @Override
            public void onFailure(Call<furnitures> call, Throwable t) {
                Log.e("API Response", "Error: " + t.getMessage());
            }
        });

        call2.enqueue(new Callback<furnitures>() {
            @Override
            public void onResponse(Call<furnitures> call, Response<furnitures> response) {
                if (response.isSuccessful()) {
                    recommendedFurniture1 = response.body();
                }
            }

            @Override
            public void onFailure(Call<furnitures> call, Throwable t) {
                Log.e("API Response", "Error: " + t.getMessage());
            }
        });

        call3.enqueue(new Callback<furnitures>() {
            @Override
            public void onResponse(Call<furnitures> call, Response<furnitures> response) {
                if (response.isSuccessful()) {
                    recommendedFurniture1 = response.body();
                }
            }

            @Override
            public void onFailure(Call<furnitures> call, Throwable t) {
                Log.e("API Response", "Error: " + t.getMessage());
            }
        });
    }

    // Open the FurnitureDetailsActivity with the selected furniture item
    private void openFurnitureDetailsActivity(furnitures furniture) {
        Intent intent = new Intent(getActivity(), FurnitureActivity.class);
        intent.putExtra("furniture", furniture);
        startActivity(intent);
    }
}