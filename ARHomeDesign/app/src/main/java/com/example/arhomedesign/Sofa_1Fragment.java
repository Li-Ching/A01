package com.example.arhomedesign;


import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;

import com.example.arhomedesign.utils.Methods;
import com.example.arhomedesign.utils.RetrofitClient;
import com.example.arhomedesign.utils.furnitures;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class Sofa_1Fragment extends Fragment {
    private TextView tvType, tvColor, tvStyle, tvBrand, tvPhoneNumber, tvAddress;

    LinearLayout back;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_sofa_1, container, false);
        back = view.findViewById(R.id.back);
        back.setOnClickListener(v -> {
            FragmentManager fragmentManager = getParentFragmentManager();
            fragmentManager.popBackStack();
        });

        tvType = view.findViewById(R.id.tvType);
        tvColor = view.findViewById(R.id.tvColor);
        tvStyle = view.findViewById(R.id.tvStyle);
        tvBrand = view.findViewById(R.id.tvBrand);
        tvPhoneNumber = view.findViewById(R.id.tvPhoneNumber);
        tvAddress = view.findViewById(R.id.tvAddress);

        Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
        Call<furnitures> call = methods.getFurniture1();
        call.enqueue(new Callback<furnitures>() {
            @Override
            public void onResponse(@NonNull Call<furnitures> call, @NonNull Response<furnitures> response) {
                furnitures furniture= response.body();
                if (response.isSuccessful() && furniture != null) {
                    Log.d("API Response", "Type: " + furniture.getType());
                    Log.d("API Response", "Color: " + furniture.getColor());
                    Log.d("API Response", "getBrand1: " + furniture.getBrand1());
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
                    Toast.makeText(getContext(), "An error has occurred", Toast.LENGTH_LONG).show();
                    Log.e("API Response", "Error: " + response.message());
                }
            }

            @Override
            public void onFailure(@NonNull Call<furnitures> call, @NonNull Throwable t) {
                Toast.makeText(getContext(), "onFailure: " + t.getMessage(), Toast.LENGTH_LONG).show();
            }
        });

        return view;
    }
    /*public static Bitmap getBitmapFromURL(String src) {
        try{
            URL url = new URL(src);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.connect();

            InputStream input = conn.getInputStream();
            Bitmap mBitmap = BitmapFactory.decodeStream(input);
            return mBitmap;
        }catch(IOException e){
            e.printStackTrace();
        }
        return null;
    }*/
}
