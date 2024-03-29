package com.example.arhomedesign.utils;

import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.arhomedesign.FurnitureActivity;
import com.example.arhomedesign.R;

import java.util.List;

public class RecommendedAdapter extends RecyclerView.Adapter<RecommendedAdapter.ViewHolder> {

    private List<furnitures> recommendedList;

    public RecommendedAdapter(List<furnitures> recommendedList) {
        this.recommendedList = recommendedList;
    }

    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.recommended_card, parent, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        holder.bind(recommendedList.get(position));
        // Populate your CardView item with furniture data here.

        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                furnitures clickedFurniture = recommendedList.get(position);

                // Handle item click here, pass the actual clicked furniture object
                Intent intent = new Intent(v.getContext(), FurnitureActivity.class);
                intent.putExtra("furniture", clickedFurniture);
                v.getContext().startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {

        return Math.min(recommendedList.size(), 3); // Ensure we don't exceed 3 items.
    }
    //FurnitureAdapter Class

    //View Holder
    class ViewHolder extends RecyclerView.ViewHolder{
        TextView furnitureName, furnitureBrand;
        ImageView furnitureImage;

        public ViewHolder(@NonNull View itemView) {
            super(itemView);
            furnitureImage = itemView.findViewById(R.id.furnitureImage);
            furnitureName = itemView.findViewById(R.id.furnitureName);
            furnitureBrand = itemView.findViewById(R.id.furnitureBrand);
        }

        public void bind(furnitures furniture) {
            String imageUrl = "http://140.137.41.136:1380/A01Web/Images/" + furniture.getPicture();

            Glide.with(itemView)
                    .load(imageUrl)
                    .into(furnitureImage);
            furnitureName.setText(furniture.getFurnitureName());
            furnitureBrand.setText(furniture.getBrand1());
        }
    }

    public void filterList(List<furnitures> filteredList) {
        recommendedList = filteredList;
        notifyDataSetChanged();
    }

}
