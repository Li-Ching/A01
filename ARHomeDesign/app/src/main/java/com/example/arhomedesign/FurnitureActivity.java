package com.example.arhomedesign;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

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

        back = findViewById(R.id.backButton);

        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

        furnitureImage = findViewById(R.id.furnitureImage);
        String imageUrl = "http://140.137.41.136:1380/A01Web/Images/sofa1.jpg";

        Glide.with(this)
                .load(imageUrl)
                .into(furnitureImage);

        tvName = findViewById(R.id.furnitureName);
        tvType = findViewById(R.id.tvType);
        tvColor = findViewById(R.id.tvColor);
        tvStyle = findViewById(R.id.tvStyle);
        tvBrand = findViewById(R.id.tvBrand);
        tvPhoneNumber = findViewById(R.id.tvPhoneNumber);
        tvAddress = findViewById(R.id.tvAddress);

        GetDataFromIntent();
    }

    private void GetDataFromIntent() {
    }
}