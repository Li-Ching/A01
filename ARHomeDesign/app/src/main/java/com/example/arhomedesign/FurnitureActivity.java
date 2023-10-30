package com.example.arhomedesign;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import com.bumptech.glide.Glide;
import com.example.arhomedesign.utils.Methods;
import com.example.arhomedesign.utils.RetrofitClient;
import com.example.arhomedesign.utils.furnitures;

public class FurnitureActivity extends AppCompatActivity {
    private TextView tvName, tvType, tvColor, tvStyle, tvBrand, tvPhoneNumber, tvAddress;
    ImageButton back;
    ImageView furnitureImage;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_furniture);
        getSupportActionBar().hide();

        tvName = findViewById(R.id.furnitureName);
        tvType = findViewById(R.id.tvType);
        tvColor = findViewById(R.id.tvColor);
        tvStyle = findViewById(R.id.tvStyle);
        tvBrand = findViewById(R.id.tvBrand);
        tvPhoneNumber = findViewById(R.id.tvPhoneNumber);
        tvAddress = findViewById(R.id.tvAddress);

        back = findViewById(R.id.backButton);

        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

        Intent intent = getIntent();
        if (intent != null && intent.hasExtra("furniture")) {
            furnitures furniture = intent.getParcelableExtra("furniture");

            // Use the furniture data to populate the UI elements in the detail activity.
            furnitureImage = findViewById(R.id.furnitureImage);
            String imageUrl = "http://140.137.41.136:1380/A01Web/Images/" + furniture.getPicture();

            Glide.with(this)
                    .load(imageUrl)
                    .into(furnitureImage);


            // Set the furniture details in the TextViews
            tvName.setText(furniture.getFurnitureName());
            tvType.setText(furniture.getType());
            tvColor.setText(furniture.getColor());
            tvStyle.setText(furniture.getStyle());
            tvBrand.setText(furniture.getBrand1());
            tvPhoneNumber.setText(furniture.getPhoneNumber());
            tvAddress.setText(furniture.getAddress());
            //GetDataFromIntent();
        }
    }

    private void GetDataFromIntent() {
        if(getIntent().hasExtra("furnitures")){
            furnitures furnitures = getIntent().getParcelableExtra("furnitures");
        }
    }
}