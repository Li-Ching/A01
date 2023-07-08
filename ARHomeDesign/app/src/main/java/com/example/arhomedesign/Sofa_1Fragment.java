package com.example.arhomedesign;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

public class Sofa_1Fragment extends Fragment {
    private TextView tvFurId, tvType, tvColor, tvStyle, tvBrand, tvPhoneNumber, tvAddress, tvLocation;

    LinearLayout back;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_sofa_1, container, false);

        tvFurId=view.findViewById(R.id.tvFurId);
        tvType=view.findViewById(R.id.tvType);
        tvColor=view.findViewById(R.id.tvColor);
        tvStyle=view.findViewById(R.id.tvStyle);
        tvBrand=view.findViewById(R.id.tvBrand);
        tvPhoneNumber=view.findViewById(R.id.tvPhoneNumber);
        tvAddress=view.findViewById(R.id.tvAddress);
        tvLocation=view.findViewById(R.id.tvLocation);

        // Call the getJokes() method of the ApiCall class,
        // passing a callback function as a parameter.
        FurnituresApiCall apiCall = new FurnituresApiCall();
        apiCall.getFurnitures(Sofa_1Fragment.this, new retrofit2.Callback<Furnitures>() {
            @Override
            public void onResponse(retrofit2.Call<Furnitures> call, retrofit2.Response<Furnitures> response) {
                // This method is called when the API response is received successfully.
                if (response.isSuccessful()) {
                    // Set the text of the text view to the
                    // joke value returned by the API response.
                    Furnitures furnitures = response.body();
                    tvFurId.setText(furnitures.furnitureId);
                    tvType.setText(furnitures.type);
                    tvColor.setText(furnitures.color);
                    tvStyle.setText(furnitures.style);
                    tvBrand.setText(furnitures.brand1);
                    tvPhoneNumber.setText(furnitures.phoneNumber);
                    tvAddress.setText(furnitures.address);
                    tvLocation.setText(furnitures.location);
                }
            }

            @Override
            public void onFailure(retrofit2.Call<Furnitures> call, Throwable t) {
                // This method is called when the API request fails.
                //Toast.makeText(Sofa_1Fragment.this, "Request Fail", Toast.LENGTH_SHORT).show();
            }
        });

        back = view.findViewById(R.id.back);

        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager fragmentManager = getParentFragmentManager();
                fragmentManager.popBackStack();

            }
        });

        return view;
    }
}