package com.example.arhomedesign;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;

import com.example.arhomedesign.Methods;
import com.example.arhomedesign.R;
import com.example.arhomedesign.RetrofitClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class Sofa_1Fragment extends Fragment {
    private TextView tvFurId, tvType, tvColor, tvStyle, tvBrand, tvPhoneNumber, tvAddress;

    LinearLayout back;



    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_sofa_1, container, false);

        back = view.findViewById(R.id.back);

        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager fragmentManager = getParentFragmentManager();
                fragmentManager.popBackStack();

            }
        });

        tvFurId = view.findViewById(R.id.tvFurId);
        tvType = view.findViewById(R.id.tvType);
        tvColor = view.findViewById(R.id.tvColor);
        tvStyle = view.findViewById(R.id.tvStyle);
        tvBrand = view.findViewById(R.id.tvBrand);
        tvPhoneNumber = view.findViewById(R.id.tvPhoneNumber);
        tvAddress = view.findViewById(R.id.tvAddress);


        super.onViewCreated(view, savedInstanceState);

        Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
        Call<furnitures> call = methods.getFurniture();
        call.enqueue(new Callback<furnitures>() {
            @Override
            public void onResponse(Call<furnitures> call, Response<furnitures> response) {
                if (response.isSuccessful()) {
                    tvFurId.setText(response.body().getFurnitureId());
                    tvType.setText(response.body().getType());
                    tvColor.setText(response.body().getColor());
                    tvStyle.setText(response.body().getStyle());
                    tvBrand.setText(response.body().getBrand1());
                    tvPhoneNumber.setText(response.body().getPhoneNumber());
                    tvAddress.setText(response.body().getAddress());
                } else {
                    Toast.makeText(getContext(), "An error has occurred", Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<furnitures> call, Throwable t) {
                Toast.makeText(getContext(), "An error has occurred", Toast.LENGTH_LONG).show();
            }
        });


        return view;
    }

}