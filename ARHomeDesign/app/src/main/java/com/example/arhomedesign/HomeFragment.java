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
    ImageButton table, chair, sofa, bed;
    LinearLayout recommend1, recommend2, recommend3;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        recommend1 = view.findViewById(R.id.butRec1);
        recommend2 = view.findViewById(R.id.butRec2);
        recommend3 = view.findViewById(R.id.butRec3);

        table = view.findViewById(R.id.btnTable);
        chair = view.findViewById(R.id.btnChair);
        sofa = view.findViewById(R.id.btnSofa);
        bed = view.findViewById(R.id.btnBed);

        TableFragment tableFragment = new TableFragment();
        FragmentManager fragmentManager = getChildFragmentManager();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out);
        transaction.replace(R.id.HomeFrame, tableFragment);
        transaction.addToBackStack(null);
        transaction.commit();

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

        table.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                TableFragment tableFragment = new TableFragment();

                FragmentManager fragmentManager = getChildFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out);
                transaction.replace(R.id.HomeFrame, tableFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        chair.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ChairFragment chairFragment = new ChairFragment();

                FragmentManager fragmentManager = getChildFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out);
                transaction.replace(R.id.HomeFrame, chairFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        sofa.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SofaFragment sofaFragment = new SofaFragment();

                FragmentManager fragmentManager = getChildFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out);
                transaction.replace(R.id.HomeFrame, sofaFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        bed.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                BedFragment bedFragment = new BedFragment();

                FragmentManager fragmentManager = getChildFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out);
                transaction.replace(R.id.HomeFrame, bedFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });


        return view;
    }
}