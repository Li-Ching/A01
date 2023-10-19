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

        Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
        Call<furnitures> call = methods.getFurniture();
        call.enqueue(new Callback<furnitures>() {
            @Override
            public void onResponse(@NonNull Call<furnitures> call, @NonNull Response<furnitures> response) {
                furnitures furniture= response.body();
                if (response.isSuccessful() && furniture != null) {
                    Log.d("API Response", "Type: " + furniture.getType());
                    Log.d("API Response", "Color: " + furniture.getColor());
                    Log.d("API Response", "getBrand1: " + furniture.getBrand1());

                    tvName.setText(furniture.getFurnitureName());
                    tvType.setText(furniture.getType());
                    tvColor.setText(furniture.getColor());
                    tvStyle.setText(furniture.getStyle());
                    tvBrand.setText(furniture.getBrand1());
                    tvPhoneNumber.setText(furniture.getPhoneNumber());
                    tvAddress.setText(furniture.getAddress());

                    /*new Thread(new Runnable(){
                        @Override
                        public void run() {

                            final Bitmap mBitmap = getBitmapFromURL(furniture.getPicture());
                            runOnUiThread(new Runnable(){
                                public void run() {
                                    ImageView mImageView = (ImageView) view.findViewById(R.id.imgSofa1);
                                    mImageView.setImageBitmap(mBitmap);
                                }}
                            );
                        }}).start();*/
                } else {
                    //Toast.makeText("An error has occurred", Toast.LENGTH_LONG).show();
                    Log.e("API Response", "Error: " + response.message());
                }
            }

            @Override
            public void onFailure(@NonNull Call<furnitures> call, @NonNull Throwable t) {
                //Toast.makeText("onFailure: " + t.getMessage(), Toast.LENGTH_LONG).show();
            }
        });
    }
}