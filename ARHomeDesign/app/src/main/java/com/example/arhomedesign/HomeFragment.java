package com.example.arhomedesign;

import android.content.Intent;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.LinearLayout;

public class HomeFragment extends Fragment {
    LinearLayout recommend1, recommend2, recommend3;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        recommend1 = view.findViewById(R.id.butRec1);
        recommend2 = view.findViewById(R.id.butRec2);
        recommend3 = view.findViewById(R.id.butRec3);

        recommend1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), FurnitureActivity.class);
                startActivity(intent);
            }
        });

        recommend2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), FurnitureActivity.class);
                startActivity(intent);
            }
        });

        recommend3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), FurnitureActivity.class);
                startActivity(intent);
            }
        });


        return view;
    }
}