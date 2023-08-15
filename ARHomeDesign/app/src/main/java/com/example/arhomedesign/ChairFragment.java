package com.example.arhomedesign;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;

public class ChairFragment extends Fragment {

    LinearLayout chair1, chair2;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_chair, container, false);

        chair1 = view.findViewById(R.id.butChair1);
        chair2 = view.findViewById(R.id.butChair2);

        chair1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Chair_1Fragment chair1Fragment = new Chair_1Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, chair1Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        chair2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Chair_2Fragment chair2Fragment = new Chair_2Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, chair2Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });



        return view;
    }
}