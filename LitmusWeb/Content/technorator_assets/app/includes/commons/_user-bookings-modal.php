<div class="modal fade user-bookings-modal" id="user_bookings_modal" tabindex="-1" role="dialog" aria-labelledby="user_bookings_modalLabel" aria-hidden="true">
	<div class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-body">
				<button type="button" class="btn-close-modal" data-dismiss="modal" aria-label="Close">
					<span class="btn-icon" aria-hidden="true">&times;</span>
				</button>
				<div class="user-bookings-container">
					<div class="bookings-inner">
						<div class="head">
							<h2>Your future bookings</h2>
						</div>
						<div class="body">
							<div class="booking-list">
								<ul class="list-unstyled">
									<?php for($i = 0; $i < 10; $i++) { ?>
										<li>
											<div class="item-content">
												<div class="content-inner">
													<div class="row row-20 row-xxs">
														
														<div class="col col-md-9 col-xs-7">
															<div class="item-locations mh_booking_content">
																<div class="locations-inner">
																	<div class="row row-20 row-650">
																		<div class="col col-sm-6">
																			<div class="item-location pickup-location">
																				<div class="location-inner">
																					<div class="location-title">
																						<h4>Pickup:</h4>
																					</div>
																					<div class="location-address location-details">
																						<div class="location-icon">
																							<span class="fa fa-plane"></span>
																						</div>
																						<div class="location-text">
																							<p>LAX, Los Angeles Intl Arpt</p>
																						</div>
																					</div>
																					<div class="location-timestamp location-details">
																						<div class="location-icon">
																							<span class="fa fa-calendar"></span>
																						</div>
																						<div class="location-text">
																							<p>2017-12-05 at 20:00</p>
																						</div>
																					</div>
																				</div>
																			</div>
																		</div>
																		<div class="col col-sm-6">
																			<div class="item-location dropoff-location">
																				<div class="location-inner">
																					<div class="location-title">
																						<h4>Dropoff:</h4>
																					</div>
																					<div class="location-address location-details">
																						<div class="location-icon">
																							<span class="fa fa-building"></span>
																						</div>
																						<div class="location-text">
																							<p>LAX, Los Angeles Intl Arpt</p>
																						</div>
																					</div>
																					<div class="location-timestamp location-details">
																						<div class="location-icon">
																							<span class="fa fa-calendar"></span>
																						</div>
																						<div class="location-text">
																							<p>2017-12-05 at 20:00</p>
																						</div>
																					</div>
																				</div>
																			</div>
																		</div>
																	</div>
																</div>
															</div>
														</div>
														<div class="col col-md-3 col-xs-5">
															<div class="item-rent-btn mh_booking_content">
																<div class="rent-btn-inner">
																	<div class="item-rent">
																		<p><span class="fa fa-dollar"></span> 79.99/-</p>
																	</div>
																	<div class="item-btn">
																		<a href="#" class="btn btn-primary btn-float">
																			<span class="btn-inner">
																				<span class="btn-text">View Details</span>
																				<span class="btn-icon"><span class="fa fa-angle-right"></span></span>
																			</span>
																		</a>
																	</div>
																</div>
															</div>
														</div>

													</div>
												</div>
											</div>
										</li>
									<?php } ?>
								</ul>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>