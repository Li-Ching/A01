package com.example.arhomedesign;

import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ProgressBar;

import androidx.appcompat.widget.SearchView;

import com.example.arhomedesign.utils.FurnituresAdapter;
import com.example.arhomedesign.utils.Methods;
import com.example.arhomedesign.utils.RetrofitClient;
import com.example.arhomedesign.utils.furnitures;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SearchFragment extends Fragment {

    RecyclerView recyclerView;
    LinearLayoutManager layoutManager;
    FurnituresAdapter adapter;
    List<furnitures> furnituresList = new ArrayList<>();
    ProgressBar progressBar;
    SearchView searchView;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_search, container, false);

        searchView = view.findViewById(R.id.search_view);
        SetupSearchView(searchView);

        progressBar = view.findViewById(R.id.progressBar);

        recyclerView = view.findViewById(R.id.recyclerView);
        layoutManager = new LinearLayoutManager(requireContext());
        recyclerView.setLayoutManager(layoutManager);
        adapter = new FurnituresAdapter(furnituresList);
        recyclerView.setAdapter(adapter);

        fetchFurnitures();

        recyclerView.addOnScrollListener(new RecyclerView.OnScrollListener() {
            @Override
            public void onScrollStateChanged(@NonNull RecyclerView recyclerView, int newState) {
                super.onScrollStateChanged(recyclerView, newState);
            }
        });

        return view;
    }


    private void fetchFurnitures(){
        progressBar.setVisibility(View.VISIBLE);
        Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
        Call<List<furnitures>> call = methods.getFurnitures();
        call.enqueue(new Callback<List<furnitures>>() {
            @Override
            public void onResponse(Call<List<furnitures>> call, Response<List<furnitures>> response) {
                if(response.isSuccessful() && response.body() != null){
                    furnituresList.addAll(response.body());
                    adapter.notifyDataSetChanged();
                    progressBar.setVisibility(View.GONE);
                }
            }

            @Override
            public void onFailure(Call<List<furnitures>> call, Throwable t) {
                progressBar.setVisibility(View.GONE);
                //Toast.makeText("An error has occurred", Toast.LENGTH_LONG).show();
                Log.e("API Response", "Error: " + t.getMessage());
            }
        });
    }

    private void SetupSearchView(SearchView search) {
         search.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
             @Override
             public boolean onQueryTextSubmit(String query) {
                 filterFurnitureList(query);
                 search.setQuery("", false); // Clear the search query
                 return true;
             }

             @Override
             public boolean onQueryTextChange(String newText) {
                 filterFurnitureList(newText);
                 return true;
             }
         });
    }

    private void filterFurnitureList(String query) {
        List<furnitures> filteredList = new ArrayList<>();

        for (furnitures furniture : furnituresList) {
            String name = furniture.getFurnitureName().toLowerCase();
            String category = furniture.getType().toLowerCase();
            String brand = furniture.getBrand1().toLowerCase();
            String color = furniture.getColor().toLowerCase();
            String style = furniture.getStyle().toLowerCase();
            query = query.toLowerCase();

            if (name.contains(query) || category.contains(query) || brand.contains(query)) {
                filteredList.add(furniture);
            }
        }

        adapter.filterList(filteredList);
    }
}