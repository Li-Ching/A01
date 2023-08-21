package com.example.arhomedesign;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;

public class SearchFragment extends Fragment {

    ImageButton table, chair, sofa, bed;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_search, container, false);

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