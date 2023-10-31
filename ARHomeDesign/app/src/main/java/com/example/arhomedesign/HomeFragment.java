package com.example.arhomedesign;

import android.content.Intent;
import android.os.Bundle;

import androidx.cardview.widget.CardView;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.LinearLayout;

import com.example.arhomedesign.utils.FurnituresAdapter;
import com.example.arhomedesign.utils.Methods;
import com.example.arhomedesign.utils.RetrofitClient;
import com.example.arhomedesign.utils.furnitures;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class HomeFragment extends Fragment {
    RecyclerView recyclerView;
    LinearLayoutManager layoutManager;
    FurnituresAdapter adapter;
    List<furnitures> recommendedList = new ArrayList<>();


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        recyclerView = view.findViewById(R.id.recyclerView);
        layoutManager = new LinearLayoutManager(requireContext(), LinearLayoutManager.HORIZONTAL, false);
        recyclerView.setLayoutManager(layoutManager);
        adapter = new FurnituresAdapter(recommendedList);
        recyclerView.setAdapter(adapter);

        // Fetch recommended furniture data
        fetchRecommendedFurnitureData();


        return view;
    }

    private void fetchRecommendedFurnitureData() {
        Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);

        // Create a list to store the calls for recommended furniture data.
        List<Call<furnitures>> furnitureCalls = new ArrayList<>();
        furnitureCalls.add(methods.getFurniture1());
        furnitureCalls.add(methods.getFurniture2());
        furnitureCalls.add(methods.getFurniture3());

        // Make the API requests for recommended furniture data.
        for (Call<furnitures> call : furnitureCalls) {
            call.enqueue(new Callback<furnitures>() {
                @Override
                public void onResponse(Call<furnitures> call, Response<furnitures> response) {
                    if (response.isSuccessful()) {
                        furnitures recommendedFurniture = response.body();
                        recommendedList.add(recommendedFurniture);
                        adapter.notifyDataSetChanged(); // Notify the adapter of data changes.
                    }
                }

                @Override
                public void onFailure(Call<furnitures> call, Throwable t) {
                    // Handle the failure case, e.g., display an error message.
                    Log.e("API Call", "Failed to fetch recommended furniture: " + t.getMessage());
                }
            });
        }
    }
}