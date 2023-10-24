<?php require_once dirname(__FILE__) . '/includes/commons/_header.php'; ?>

	<section class="search-container searchbar-container">
		<div class="container">
			<div class="search-inner searchbar-inner">
				<div class="search-toggler">
					<span class="btn-search-toggler" data-fn-toggle data-fn-target="#search_form">Search Filters</span>
				</div>
				<div class="search-form" id="search_form">
					<div class="form-inner">
						<form action="#" class="global-form">
							<div class="row row-10">
								<div class="col col-lg-4">
									<div class="locations-container dropoff-active" id="cnt_location_search">
										<div class="locations-inner">
											<div class="locations-group">
												<div class="form-group pickup-group">
													<div class="input-group">
														<input type="text" name="pickup_location" id="pickup_location" class="form-control" placeholder="Pickup Location"/>
														<span class="group-addon"><span class="fa fa-map-marker"></span></span>
													</div>
												</div>
												<div class="form-group dropoff-group" style="display: block;">
													<div class="input-group">
														<input type="text" name="dropoff_location" id="dropoff_location" class="form-control" placeholder="Dropoff Location"/>
														<span class="group-addon"><span class="fa fa-map-marker"></span></span>
													</div>
												</div>
											</div>
											<!-- <div class="location-toggler">
												<label class="btn-location-toggler form-checkbox-label" id="btn_location_toggler">
													<input type="checkbox" name="dropoff_location_checkbox" class="dropoff_location_checkbox"/>
													<span class="btn-text">Return car to different location</span>
												</label>
											</div> -->
										</div>
									</div>
								</div>
								<div class="col col-lg-6 col-sm-10">
									<div class="row row-10 row-xs">
										<div class="col col-xs-3">
											<div class="form-group">
												<div class="input-group">
													<input type="text" name="pickup_date" id="pickup_date" class="form-control" placeholder="Pickup Date"/>
													<span class="group-addon"><span class="fa fa-calendar"></span></span>
												</div>
											</div>
										</div>
										<div class="col col-xs-3">
											<div class="form-group">
												<div class="input-group">
													<select name="pickup_location_time" id="pickup_location_time" class="form-control">
														<option>10:00 AM</option>
														<option>11:00 AM</option>
														<option>12:00 PM</option>
													</select>
													<span class="group-addon"><span class="fa fa-clock-o"></span></span>
												</div>
											</div>
										</div>
										<div class="col col-xs-3">
											<div class="form-group">
												<div class="input-group">
													<input type="text" name="dropoff_date" id="dropoff_date" class="form-control" placeholder="Dropoff Date"/>
													<span class="group-addon"><span class="fa fa-calendar"></span></span>
												</div>
											</div>
										</div>
										<div class="col col-xs-3">
											<div class="form-group">
												<div class="input-group">
													<select name="dropoff_location_time" id="dropoff_location_time" class="form-control">
														<option>10:00 AM</option>
														<option>11:00 AM</option>
														<option>12:00 PM</option>
													</select>
													<span class="group-addon"><span class="fa fa-clock-o"></span></span>
												</div>
											</div>
										</div>
									</div>
								</div>
								<div class="col col-lg-2 col-sm-2">
									<div class="btn-submit">
										<div class="btn-inner">
											<div class="btn-wrapper">
												<button class="btn btn-primary btn-block">Update</button>
											</div>
										</div>
									</div>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
	</section>

	<section class="car-listing-container">
		<div class="car-listing-inner">
			
			<div class="sorting-container" hidden>
				<div class="container">
					<div class="sorting-inner">
						<div class="sort-content">
							<div class="sort-title">
								<p>Sort By:</p>
							</div>
							<div class="sort-filter" hidden>
								<div class="sort-filter-inner">
									<div class="filter-toggler">
										<a href="#" data-fn-toggle data-fn-target="#sort_filter_list">Sort By</a>
									</div>
									<div class="filter-list" id="sort_filter_list" style="display: none;">
										<ul class="list-unstyled">
											<li data-value="relevance">Relevance</li>
											<li data-value="price_high">Price High to Low</li>
											<li data-value="price_low">Price Low to High</li>
											<li data-value="discount_high">Discount High to Low</li>
											<li data-value="discount_low">Discount Low to High</li>
										</ul>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="listing-blocks-container">
				<div class="container">
					<div class="blocks-inner">
						
						<?php for($i = 0; $i < 10; $i++) { ?>
							<div class="listing-block car-listing-block block-horizontal">
								<div class="block-inner">
									
									<div class="block-title-meta hidden-md-up">
										<div class="title-meta-inner">
											<div class="block-title">
												<h3>Car Type <small>Car Name</small></h3>
											</div>
											<div class="block-meta">
												<ul class="list-inline">
													<li>
														<span class="meta-icon">
															<span class="fa fa-users"></span>
														</span>
														<span class="meta-text">5</span>
													</li>
													<li>
														<span class="meta-icon">
															<span class="fa fa-suitcase"></span>
														</span>
														<span class="meta-text">5</span>
													</li>
													<li class="meta-sm">
														<span class="meta-icon">
															<span class="fa fa-suitcase"></span>
														</span>
														<span class="meta-text">5</span>
													</li>
												</ul>
											</div>
										</div>
									</div>
									<div class="row row-20 row-listing row-xxs">
										<div class="col col-md-3 col-xs-7">
											<div class="block-media mh_listing_block_content">
												<div class="block-logo">
													<a href="https://hotwire.com" target="_blank"><img src="assets/images/logos/hotwire-logo.png" class="img-responsive" alt=""/></a>
												</div>
												<div class="block-image">
													<img src="assets/images/cars/car-01.png" class="img-responsive" alt=""/>
												</div>
											</div>
										</div>
										<div class="col col-md-9 col-xs-5">
											<div class="block-content mh_listing_block_content">
												<div class="content-inner">
													<div class="row row-20">
														
														<div class="col col-md-8">
															<div class="block-title-meta hidden-sm-down">
																<div class="title-meta-inner">
																	<div class="block-title">
																		<h3>Car Type <small>Car Name</small></h3>
																	</div>
																	<div class="block-meta">
																		<ul class="list-inline">
																			<li>
																				<span class="meta-icon">
																					<span class="fa fa-users"></span>
																				</span>
																				<span class="meta-text">5</span>
																			</li>
																			<li>
																				<span class="meta-icon">
																					<span class="fa fa-suitcase"></span>
																				</span>
																				<span class="meta-text">5</span>
																			</li>
																			<li class="meta-sm">
																				<span class="meta-icon">
																					<span class="fa fa-suitcase"></span>
																				</span>
																				<span class="meta-text">5</span>
																			</li>
																		</ul>
																	</div>
																</div>
															</div>
															<div class="block-features-locations hidden-sm-down">
																<div class="features-locations-inner">
																	<div class="row row-10">
																		
																		<div class="col col-lg-5 col-md-6">
																			<div class="block-features">
																				<div class="features-inner">
																					<div class="features-title">
																						<h4>Features:</h4>
																					</div>
																					<div class="features-list">
																						<ul class="list-unstyled">
																							<li>Air Conditioning</li>
																							<li>Auto Transmission</li>
																							<li>Unlimited Mileage</li>
																						</ul>
																					</div>
																				</div>
																			</div>
																		</div>
																		<div class="col col-lg-7 col-md-6">
																			<div class="block-locations">
																				<div class="block-location pickup-location">
																					<div class="location-inner">
																						<div class="location-title">
																							<h4>From:</h4>
																						</div>
																						<div class="location-address">
																							<div class="location-icon">
																								<span class="fa fa-plane"></span>
																							</div>
																							<div class="location-text">
																								<p>LAX, Los Angeles Intl Arpt</p>
																							</div>
																						</div>
																					</div>
																				</div>
																				<div class="block-location dropoff-location">
																					<div class="location-inner">
																						<div class="location-title">
																							<h4>To:</h4>
																						</div>
																						<div class="location-address">
																							<div class="location-icon">
																								<span class="fa fa-building"></span>
																							</div>
																							<div class="location-text">
																								<p>LAX, Los Angeles Intl Arpt</p>
																							</div>
																						</div>
																					</div>
																				</div>
																			</div>
																		</div>

																	</div>
																</div>
															</div>
														</div>
														<div class="col col-md-4">
															<div class="block-booking-meta">
																<div class="booking-meta-inner">
																	
																	<div class="booking-rate">
																		<p><span>20</span> / day</p>
																	</div>
																	<div class="booking-discount">
																		<div class="discount-text total-amt">
																			<span class="fa fa-dollar"></span>
																			<p><span>60</span> Total</p>
																		</div>
																		<div class="discount-text discount-amt">
																			<span class="fa fa-dollar"></span>
																			<p><span>5</span> Cashback</p>
																		</div>
																	</div>
																	<div class="booking-total">
																		<p><span>55</span> total</p>
																	</div>
																	<div class="booking-btn">
																		<a href="#" class="btn btn-book btn-danger btn-block btn-float">Book</a>
																	</div>

																</div>
															</div>
														</div>

													</div>
												</div>
											</div>
										</div>
									</div>

								</div>
							</div>
						<?php } ?>

					</div>
				</div>
			</div>

		</div>
	</section>

<?php require_once dirname(__FILE__) . '/includes/commons/_footer.php'; ?>