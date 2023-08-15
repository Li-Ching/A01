package com.example.arhomedesign;

import android.os.Bundle;

import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;

public class HomeFragment extends Fragment {
    Button table, chair, sofa, bed;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        table = view.findViewById(R.id.btnTable);
        chair = view.findViewById(R.id.btnChair);
        sofa = view.findViewById(R.id.btnSofa);
        bed = view.findViewById(R.id.btnBed);

        AllFragment allFragment = new AllFragment();
        FragmentManager fragmentManager = getChildFragmentManager();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out);
        transaction.replace(R.id.FrameLayout, allFragment);
        transaction.addToBackStack(null);
        transaction.commit();

        table.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                TableFragment tableFragment = new TableFragment();

                FragmentManager fragmentManager = getChildFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out);
                transaction.replace(R.id.FrameLayout, tableFragment);
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
                transaction.replace(R.id.FrameLayout, chairFragment);
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
                transaction.replace(R.id.FrameLayout, sofaFragment);
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
                transaction.replace(R.id.FrameLayout, bedFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });


        return view;
    }
}